from pathlib import Path
import csv
import sys

import pytest

PROJECT_ROOT = Path(__file__).resolve().parents[1]
if str(PROJECT_ROOT) not in sys.path:
    sys.path.insert(0, str(PROJECT_ROOT))

from scripts.generate_process_recipes_full import generate_process_recipes


def _latest_package_root() -> Path:
    candidates = sorted((PROJECT_ROOT / "out_repo_mapping").glob("20*"), reverse=True)
    if not candidates:
        raise RuntimeError("No se encontró ningún paquete en out_repo_mapping")
    return candidates[0]


PKG_ROOT = _latest_package_root()
CATALOG_PATH = PKG_ROOT / "process_catalog_full.csv"
SIGNALS_PATH = PKG_ROOT / "complexity_metrics.csv"


@pytest.fixture(scope="module")
def generated_output(tmp_path_factory):
    out_dir = tmp_path_factory.mktemp("r9")
    out_path = out_dir / "process_recipes_full.csv"
    records = generate_process_recipes(CATALOG_PATH, SIGNALS_PATH, out_path)
    return records, out_path


def test_generates_row_per_process(generated_output):
    records, out_path = generated_output
    with CATALOG_PATH.open(newline="", encoding="utf-8") as handle:
        catalog_count = sum(1 for _ in csv.DictReader(handle))
    assert len(records) == catalog_count

    with out_path.open(newline="", encoding="utf-8") as handle:
        reader = csv.DictReader(handle)
        assert reader.fieldnames == [
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
        output_rows = list(reader)
    assert len(output_rows) == catalog_count


def test_simple_process_estimations(generated_output):
    records, _ = generated_output
    target = records[0]
    assert target["process_id"] == "inventario.inventario_recepcion_registrar.guardar"
    assert target["base_hours"] == 8
    assert target["adj_report"] == 0
    assert target["adj_export"] == 0
    assert target["adj_devexpress"] == 0
    assert target["adj_upload"] == 0
    assert target["adj_validation"] == 0
    assert target["complexity_score"] == "3.4"
    assert target["est_hours_total"] == 11
    assert target["role_mix"] == '{"FE":0.35,"BE":0.35,"QA":0.2,"PM":0.1}'
    assert target["risk_flags"] == ""


def test_adjustments_and_risks(generated_output):
    records, _ = generated_output
    for record in records:
        assert record["adj_report"] == 0
        assert record["adj_export"] == 0
        assert record["adj_devexpress"] == 0
        assert record["adj_upload"] == 0
        assert record["adj_validation"] == 0
        assert record["risk_flags"] == ""


def test_estimation_respects_complexity_rounding(generated_output):
    records, _ = generated_output
    for record in records[:10]:
        expected_total = record["base_hours"] + int(round(float(record["complexity_score"])))
        assert record["est_hours_total"] == expected_total
