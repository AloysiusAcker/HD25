#!/usr/bin/env python3
"""Generate process_recipes_full.csv from catalog and complexity signals."""
from __future__ import annotations

import csv
import json
import math
import sys
from dataclasses import dataclass
from decimal import Decimal, ROUND_HALF_UP
from pathlib import Path
from typing import Dict, Iterable, List, Optional

BASE_HOURS = {
    "LIST": 8,
    "CREATE": 12,
    "EDIT": 12,
    "DETAIL": 6,
    "REPORT": 10,
    "EXPORT": 4,
}

VERB_ALIASES = {
    "REGISTRAR": "CREATE",
    "CREAR": "CREATE",
    "GUARDAR": "CREATE",
    "GRABAR": "CREATE",
    "RESOLVER": "CREATE",
    "ACTUALIZAR": "EDIT",
    "EDITAR": "EDIT",
    "MODIFICAR": "EDIT",
    "ELIMINAR": "EDIT",
    "BORRAR": "EDIT",
    "LIMPIAR": "EDIT",
    "DETALLE": "DETAIL",
    "VER": "DETAIL",
    "CONSULTAR": "DETAIL",
    "EJECUTAR": "DETAIL",
    "LISTAR": "LIST",
    "REPORTE": "REPORT",
    "REPORTAR": "REPORT",
    "EXPORTAR": "EXPORT",
}

DEFAULT_ROLE_MIX = {
    "FE": 0.35,
    "BE": 0.35,
    "QA": 0.20,
    "PM": 0.10,
}

@dataclass
class CatalogEntry:
    process_id: str
    action: str
    verb: str
    noun: str
    notes: str

@dataclass
class ComplexitySignals:
    loc: int
    n_grid_like: int
    n_sql_calls_hint: int
    n_reports: int
    n_devexpress: int
    n_file_uploads: int
    n_form_controls: int
    has_codebehind: bool


def read_catalog(path: Path) -> Dict[str, CatalogEntry]:
    entries: Dict[str, CatalogEntry] = {}
    with path.open(newline="", encoding="utf-8") as fh:
        reader = csv.DictReader(fh)
        required = {"process_id", "action", "verb", "noun", "notes"}
        missing = required - set(reader.fieldnames or [])
        if missing:
            raise ValueError(f"Catalog '{path}' missing columns: {sorted(missing)}")
        for row in reader:
            process_id = (row.get("process_id") or "").strip()
            if not process_id:
                continue
            entry = CatalogEntry(
                process_id=process_id,
                action=(row.get("action") or "").strip(),
                verb=(row.get("verb") or "").strip(),
                noun=(row.get("noun") or "").strip(),
                notes=(row.get("notes") or "").strip(),
            )
            entries[process_id] = entry
    return entries


def read_complexity(path: Path) -> Dict[str, ComplexitySignals]:
    signals: Dict[str, ComplexitySignals] = {}
    with path.open(newline="", encoding="utf-8") as fh:
        reader = csv.DictReader(fh)
        required = {
            "process_id",
            "loc",
            "n_grid_like",
            "n_sql_calls_hint",
            "n_reports",
            "n_devexpress",
            "n_file_uploads",
            "n_form_controls",
            "has_codebehind",
        }
        missing = required - set(reader.fieldnames or [])
        if missing:
            raise ValueError(f"Complexity '{path}' missing columns: {sorted(missing)}")
        for row in reader:
            process_id = (row.get("process_id") or "").strip()
            if not process_id:
                continue
            def _as_int(key: str) -> int:
                value = row.get(key)
                if value is None or value == "":
                    return 0
                return int(float(value))
            signals[process_id] = ComplexitySignals(
                loc=_as_int("loc"),
                n_grid_like=_as_int("n_grid_like"),
                n_sql_calls_hint=_as_int("n_sql_calls_hint"),
                n_reports=_as_int("n_reports"),
                n_devexpress=_as_int("n_devexpress"),
                n_file_uploads=_as_int("n_file_uploads"),
                n_form_controls=_as_int("n_form_controls"),
                has_codebehind=(_as_int("has_codebehind") != 0),
            )
    return signals


def normalize_verb(raw: str) -> str:
    if not raw:
        raise ValueError("Missing verb value")
    key = raw.strip().upper()
    if key in BASE_HOURS:
        return key
    alias = VERB_ALIASES.get(key)
    if alias:
        return alias
    raise ValueError(f"Unsupported verb '{raw}'")


def detect_export(entry: CatalogEntry) -> bool:
    combined = " ".join([entry.action, entry.noun, entry.notes]).lower()
    keywords = ("export", "excel", "descargar", "csv")
    return any(kw in combined for kw in keywords)


def round_half_up(value: float) -> int:
    return int(Decimal(value).quantize(Decimal("1"), rounding=ROUND_HALF_UP))


def compute_role_mix(heavy_sql: bool, reporting: bool, file_upload: bool) -> str:
    mix = DEFAULT_ROLE_MIX.copy()
    if heavy_sql:
        shift = 0.10
        mix["FE"] = max(0.0, mix["FE"] - shift)
        mix["BE"] = mix["BE"] + shift
    if reporting:
        shift = 0.05
        mix["FE"] = max(0.0, mix["FE"] - shift)
        mix["BE"] = mix["BE"] + shift
    if file_upload:
        # Small QA bump to cover edge cases for file handling
        shift = 0.03
        mix["QA"] = mix["QA"] + shift
        mix["PM"] = max(0.0, mix["PM"] - shift)
    # Normalize to sum to 1.0
    total = sum(mix.values())
    if total <= 0:
        raise ValueError("Invalid role mix total")
    mix = {key: round(value / total, 2) for key, value in mix.items()}
    # Adjust rounding drift
    drift = round(1.0 - sum(mix.values()), 2)
    if abs(drift) >= 0.01:
        mix["BE"] = round(mix["BE"] + drift, 2)
    elif drift:
        mix["BE"] = round(mix["BE"] + drift, 2)
    return json.dumps(mix, separators=(",", ":"))


def build_rows(catalog: Dict[str, CatalogEntry], complexity: Dict[str, ComplexitySignals]) -> List[Dict[str, object]]:
    rows: List[Dict[str, object]] = []
    for process_id, entry in catalog.items():
        signals = complexity.get(process_id, ComplexitySignals(0, 0, 0, 0, 0, 0, 0, False))
        try:
            norm_verb = normalize_verb(entry.verb)
        except ValueError as exc:
            raise ValueError(f"{process_id}: {exc}") from exc
        base_hours = BASE_HOURS[norm_verb]
        adj_report = 4 if signals.n_reports > 0 else 0
        has_export_trait = norm_verb == "EXPORT" or detect_export(entry)
        adj_export = 2 if has_export_trait else 0
        adj_devexpress = 3 if signals.n_devexpress > 0 else 0
        adj_upload = 2 if signals.n_file_uploads > 0 else 0
        adj_validation = 1 if signals.n_form_controls > 8 else 0
        adj_crud = 0
        complexity_score = math.log10(1 + max(signals.loc, 0))
        complexity_score += 0.2 * signals.n_grid_like
        complexity_score += 0.1 * signals.n_sql_calls_hint
        complexity_score = round(complexity_score, 2)
        total_adjustments = (
            adj_crud
            + adj_report
            + adj_export
            + adj_devexpress
            + adj_upload
            + adj_validation
        )
        est_hours_total = base_hours + total_adjustments + round_half_up(complexity_score)
        heavy_sql = signals.n_sql_calls_hint >= 20
        reporting_flag = signals.n_reports > 0
        file_upload_flag = signals.n_file_uploads > 0
        risk_flags: List[str] = []
        if not signals.has_codebehind:
            risk_flags.append("missing_codebehind")
        if heavy_sql:
            risk_flags.append("heavy_sql")
        if reporting_flag:
            risk_flags.append("reporting")
        if file_upload_flag:
            risk_flags.append("file_uploads")
        risk_flags_str = "|".join(risk_flags)
        role_mix = compute_role_mix(heavy_sql, reporting_flag, file_upload_flag)
        row = {
            "process_id": process_id,
            "base_hours": base_hours,
            "adj_crud": adj_crud,
            "adj_report": adj_report,
            "adj_export": adj_export,
            "adj_devexpress": adj_devexpress,
            "adj_upload": adj_upload,
            "adj_validation": adj_validation,
            "complexity_score": f"{complexity_score:.2f}",
            "est_hours_total": est_hours_total,
            "role_mix": role_mix,
            "risk_flags": risk_flags_str,
        }
        rows.append(row)
    rows.sort(key=lambda r: r["process_id"])
    return rows


def write_csv(path: Path, rows: Iterable[Dict[str, object]]) -> None:
    fieldnames = [
        "process_id",
        "base_hours",
        "adj_crud",
        "adj_report",
        "adj_export",
        "adj_devexpress",
        "adj_upload",
        "adj_validation",
        "complexity_score",
        "est_hours_total",
        "role_mix",
        "risk_flags",
    ]
    with path.open("w", newline="", encoding="utf-8") as fh:
        writer = csv.DictWriter(fh, fieldnames=fieldnames)
        writer.writeheader()
        for row in rows:
            writer.writerow(row)


def main(argv: Optional[List[str]] = None) -> int:
    argv = list(sys.argv[1:] if argv is None else argv)
    catalog_path = Path("process_catalog_full.csv")
    complexity_path = Path("complexity_metrics.csv")
    output_path = Path("process_recipes_full.csv")
    catalog = read_catalog(catalog_path)
    complexity = read_complexity(complexity_path)
    rows = build_rows(catalog, complexity)
    write_csv(output_path, rows)
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
