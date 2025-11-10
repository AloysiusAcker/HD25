from __future__ import annotations

import csv
from collections import defaultdict
from decimal import Decimal, InvalidOperation
from pathlib import Path
from typing import Dict, Iterable, List, Tuple

BASE_DIR = Path(__file__).resolve().parent
CATALOG_CANDIDATES = [
    "process_catalog_full.csv",
    "process_catalog.csv",
]
RECIPES_CANDIDATES = [
    "process_recipes_full.csv",
    "process_recipes.csv",
    "process_recipes_seed.csv",
]
OUTPUT_PROCESSES = "view_costeo_procesos.csv"
OUTPUT_MODULES = "view_costeo_modulos.csv"


class CosteoDataError(RuntimeError):
    """Raised when required data is missing or malformed."""


def resolve_path(candidates: Iterable[str]) -> Path:
    for name in candidates:
        path = BASE_DIR / name
        if path.exists():
            return path
    raise FileNotFoundError(
        "None of the expected files exist: " + ", ".join(candidates)
    )


def parse_decimal(value: str | None) -> Decimal:
    if value is None:
        raise CosteoDataError("Missing numeric value")
    value = value.strip()
    if not value:
        raise CosteoDataError("Empty numeric value")
    try:
        return Decimal(value)
    except InvalidOperation as exc:  # pragma: no cover - defensive branch
        raise CosteoDataError(f"Invalid numeric value: {value!r}") from exc


def extract_hours(row: Dict[str, str]) -> Decimal:
    hour_keys = [
        "est_hours_total",
        "est_hours",
        "hours_total",
        "estimated_hours",
    ]
    for key in hour_keys:
        if key in row and row[key].strip():
            return parse_decimal(row[key])

    cpu_value = row.get("cpu_ms_est", "").strip()
    if cpu_value:
        try:
            cpu_ms = Decimal(cpu_value)
        except InvalidOperation as exc:  # pragma: no cover - defensive branch
            raise CosteoDataError(
                f"Invalid cpu_ms_est value for process {row.get('process_id')!r}: {cpu_value!r}"
            ) from exc
        # Convert CPU milliseconds to hours (1 hour = 3_600_000 ms)
        return cpu_ms / Decimal(3_600_000)

    raise CosteoDataError(
        f"No hours estimation available for process {row.get('process_id')!r}"
    )


def format_decimal(value: Decimal) -> str:
    normalized = value.normalize()
    text = format(normalized, "f")
    if "." in text:
        text = text.rstrip("0").rstrip(".")
    return text or "0"


def load_catalog(path: Path) -> Dict[str, Dict[str, str]]:
    with path.open(newline="", encoding="utf-8") as fh:
        reader = csv.DictReader(fh)
        catalog = {}
        for row in reader:
            process_id = row.get("process_id")
            if not process_id:
                continue
            catalog[process_id] = row
    return catalog


def build_views() -> Tuple[List[Dict[str, str]], List[Dict[str, str]]]:
    catalog_path = resolve_path(CATALOG_CANDIDATES)
    recipes_path = resolve_path(RECIPES_CANDIDATES)

    catalog = load_catalog(catalog_path)

    process_rows: List[Dict[str, str]] = []
    module_agg: Dict[str, Dict[str, Decimal | int]] = defaultdict(
        lambda: {"procesos_count": 0, "horas_totales_estimadas": Decimal("0")}
    )

    with recipes_path.open(newline="", encoding="utf-8") as fh:
        reader = csv.DictReader(fh)
        for row in reader:
            process_id = row.get("process_id")
            if not process_id:
                continue
            catalog_entry = catalog.get(process_id)
            if not catalog_entry:
                continue

            module_guess = (
                catalog_entry.get("module_guess")
                or catalog_entry.get("module")
                or ""
            )
            verb = catalog_entry.get("verb", "")
            try:
                hours = extract_hours(row)
            except CosteoDataError:
                continue

            process_rows.append(
                {
                    "module_guess": module_guess,
                    "process_id": process_id,
                    "verb": verb,
                    "est_hours_total": format_decimal(hours),
                }
            )

            module_entry = module_agg[module_guess]
            module_entry["procesos_count"] = int(module_entry["procesos_count"]) + 1
            module_entry["horas_totales_estimadas"] = (
                module_entry["horas_totales_estimadas"] + hours
            )

    process_rows.sort(key=lambda r: (r["module_guess"], r["process_id"]))

    module_rows = []
    for module_guess, data in module_agg.items():
        module_rows.append(
            {
                "module_guess": module_guess,
                "procesos_count": str(data["procesos_count"]),
                "horas_totales_estimadas": format_decimal(
                    data["horas_totales_estimadas"]
                ),
            }
        )

    module_rows.sort(
        key=lambda r: (
            Decimal(r["horas_totales_estimadas"] or "0"),
            r["module_guess"],
        ),
        reverse=True,
    )

    return process_rows, module_rows


def write_csv(path: Path, fieldnames: List[str], rows: List[Dict[str, str]]) -> None:
    with path.open("w", newline="", encoding="utf-8") as fh:
        writer = csv.DictWriter(fh, fieldnames=fieldnames, lineterminator="\n")
        writer.writeheader()
        writer.writerows(rows)


def main() -> None:
    process_rows, module_rows = build_views()
    write_csv(
        BASE_DIR / OUTPUT_PROCESSES,
        ["module_guess", "process_id", "verb", "est_hours_total"],
        process_rows,
    )
    write_csv(
        BASE_DIR / OUTPUT_MODULES,
        ["module_guess", "procesos_count", "horas_totales_estimadas"],
        module_rows,
    )


if __name__ == "__main__":
    main()
