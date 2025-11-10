#!/usr/bin/env python3
"""Generate process effort estimation recipes from catalog and complexity signals."""

from __future__ import annotations

import argparse
import csv
import json
import math
from dataclasses import dataclass
from pathlib import Path
from typing import Dict, Iterable, List, Sequence

BASE_HOURS_BY_VERB = {
    "LIST": 8,
    "CREATE": 12,
    "EDIT": 12,
    "DETAIL": 6,
    "REPORT": 10,
    "EXPORT": 4,
}

ROLE_MIX_JSON = json.dumps({"FE": 0.35, "BE": 0.35, "QA": 0.2, "PM": 0.1}, separators=(",", ":"))


@dataclass
class ComplexitySignals:
    loc: int = 0
    n_grid_like: int = 0
    n_form_controls: int = 0
    n_file_uploads: int = 0
    n_reports: int = 0
    n_devexpress: int = 0
    n_sql_calls_hint: int = 0

    def add(self, other: "ComplexitySignals") -> None:
        self.loc += other.loc
        self.n_grid_like += other.n_grid_like
        self.n_form_controls += other.n_form_controls
        self.n_file_uploads += other.n_file_uploads
        self.n_reports += other.n_reports
        self.n_devexpress += other.n_devexpress
        self.n_sql_calls_hint += other.n_sql_calls_hint


def parse_args(argv: Sequence[str] | None = None) -> argparse.Namespace:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--catalog", required=True, help="Path to process_catalog_full.csv")
    parser.add_argument("--signals", required=True, help="Path to complexity_metrics.csv")
    parser.add_argument("--out", required=True, help="Output CSV path")
    return parser.parse_args(argv)


def load_complexity_signals(path: Path) -> Dict[str, ComplexitySignals]:
    metrics: Dict[str, ComplexitySignals] = {}
    with path.open(newline="", encoding="utf-8") as handle:
        reader = csv.DictReader(handle)
        for row in reader:
            key = row["path_rel"].strip()
            if not key:
                continue
            signals = ComplexitySignals(
                loc=int(row.get("loc", 0) or 0),
                n_grid_like=int(row.get("n_grid_like", 0) or 0),
                n_form_controls=int(row.get("n_form_controls", 0) or 0),
                n_file_uploads=int(row.get("n_file_uploads", 0) or 0),
                n_reports=int(row.get("n_reports", 0) or 0),
                n_devexpress=int(row.get("n_devexpress", 0) or 0),
                n_sql_calls_hint=int(row.get("n_sql_calls_hint", 0) or 0),
            )
            if key in metrics:
                metrics[key].add(signals)
            else:
                metrics[key] = signals
    return metrics


def infer_verb(row: Dict[str, str]) -> str:
    verb = (row.get("verb") or "").strip().upper()
    if verb in BASE_HOURS_BY_VERB:
        return verb
    page_name = (row.get("page_name") or "").lower()
    path_aspx = (row.get("path_aspx") or "").lower()
    for token, mapped in (
        ("export", "EXPORT"),
        ("report", "REPORT"),
        ("detail", "DETAIL"),
        ("edit", "EDIT"),
        ("update", "EDIT"),
        ("create", "CREATE"),
        ("new", "CREATE"),
    ):
        if token in page_name or token in path_aspx:
            return mapped
    return "LIST"


def gather_paths(row: Dict[str, str]) -> List[str]:
    paths: List[str] = []
    for key in ("path_aspx", "path_vb"):
        value = (row.get(key) or "").strip()
        if value:
            paths.append(value)
            if key == "path_aspx" and value.lower().endswith(".aspx"):
                designer = f"{value}.designer.vb"
                paths.append(designer)
    return paths


def collect_signals(paths: Iterable[str], metrics: Dict[str, ComplexitySignals]) -> ComplexitySignals:
    total = ComplexitySignals()
    for path in paths:
        if path in metrics:
            total.add(metrics[path])
    return total


def compute_base_hours(verb: str) -> int:
    return BASE_HOURS_BY_VERB.get(verb, BASE_HOURS_BY_VERB["LIST"])


def detect_export(row: Dict[str, str], inferred_verb: str) -> bool:
    if inferred_verb == "EXPORT":
        return True
    page_name = (row.get("page_name") or "").lower()
    path_aspx = (row.get("path_aspx") or "").lower()
    return "export" in page_name or "export" in path_aspx


def compute_adjustments(row: Dict[str, str], signals: ComplexitySignals, verb: str) -> Dict[str, int]:
    adjustments = {
        "adj_crud": 0,
        "adj_report": 4 if signals.n_reports > 0 else 0,
        "adj_export": 2 if detect_export(row, verb) else 0,
        "adj_devexpress": 3 if signals.n_devexpress > 0 else 0,
        "adj_upload": 2 if signals.n_file_uploads > 0 else 0,
        "adj_validation": 1 if signals.n_form_controls > 8 else 0,
    }
    return adjustments


def compute_complexity_score(signals: ComplexitySignals) -> float:
    score = math.log10(1 + max(signals.loc, 0))
    score += 0.2 * signals.n_grid_like
    score += 0.1 * signals.n_sql_calls_hint
    return score


def compute_risk_flags(row: Dict[str, str], signals: ComplexitySignals, verb: str) -> str:
    flags = []
    if not (row.get("path_vb") or "").strip():
        flags.append("missing_codebehind")
    if signals.n_sql_calls_hint > 10:
        flags.append("heavy_sql")
    if signals.n_reports > 0 or verb == "REPORT":
        flags.append("reporting")
    if signals.n_file_uploads > 0:
        flags.append("file_uploads")
    return "|".join(flags)


def build_record(row: Dict[str, str], metrics: Dict[str, ComplexitySignals]) -> Dict[str, object]:
    verb = infer_verb(row)
    base_hours = compute_base_hours(verb)
    signals = collect_signals(gather_paths(row), metrics)
    adjustments = compute_adjustments(row, signals, verb)
    complexity_score = compute_complexity_score(signals)
    complexity_score_display = f"{complexity_score:.1f}"
    total_adjustments = sum(adjustments.values())
    est_hours_total = base_hours + total_adjustments + int(round(complexity_score))
    risk_flags = compute_risk_flags(row, signals, verb)

    return {
        "process_id": row["process_id"],
        "base_hours": base_hours,
        "adj_crud": adjustments["adj_crud"],
        "adj_report": adjustments["adj_report"],
        "adj_export": adjustments["adj_export"],
        "adj_devexpress": adjustments["adj_devexpress"],
        "adj_upload": adjustments["adj_upload"],
        "adj_validation": adjustments["adj_validation"],
        "complexity_score": complexity_score_display,
        "est_hours_total": est_hours_total,
        "role_mix": ROLE_MIX_JSON,
        "risk_flags": risk_flags,
    }


def generate_process_recipes(catalog_path: Path, signals_path: Path, out_path: Path) -> List[Dict[str, object]]:
    metrics = load_complexity_signals(signals_path)
    records: List[Dict[str, object]] = []
    with catalog_path.open(newline="", encoding="utf-8") as handle:
        reader = csv.DictReader(handle)
        for row in reader:
            record = build_record(row, metrics)
            records.append(record)

    out_path.parent.mkdir(parents=True, exist_ok=True)
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
    with out_path.open("w", newline="", encoding="utf-8") as handle:
        writer = csv.DictWriter(handle, fieldnames=fieldnames)
        writer.writeheader()
        for record in records:
            writer.writerow(record)
    return records


def main(argv: Sequence[str] | None = None) -> None:
    args = parse_args(argv)
    generate_process_recipes(Path(args.catalog), Path(args.signals), Path(args.out))


if __name__ == "__main__":
    main()
