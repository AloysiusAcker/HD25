from pathlib import Path
import csv
import sys

import pytest

PROJECT_ROOT = Path(__file__).resolve().parents[1]
if str(PROJECT_ROOT) not in sys.path:
    sys.path.insert(0, str(PROJECT_ROOT))

from scripts.generate_process_recipes_full import generate_process_recipes

CATALOG_PATH = Path("process_catalog_full.csv")
SIGNALS_PATH = Path("complexity_metrics.csv")


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
    target = next(r for r in records if r["process_id"] == "01c5c8994c7e5a4dc3b721f7f7493e333b7b9abd")
    assert target["base_hours"] == 8
    assert target["adj_report"] == 0
    assert target["adj_export"] == 0
    assert target["adj_devexpress"] == 0
    assert target["adj_upload"] == 0
    assert target["adj_validation"] == 0
    assert target["complexity_score"] == "1.5"
    assert target["est_hours_total"] == 10
    assert target["role_mix"] == '{"FE":0.35,"BE":0.35,"QA":0.2,"PM":0.1}'
    assert target["risk_flags"] == ""


def test_adjustments_and_risks(generated_output):
    records, _ = generated_output
    target = next(r for r in records if r["process_id"] == "b63fad7c6ffbd94a1d7bad1660363a586b89698b")
    assert target["adj_devexpress"] == 3
    assert target["adj_upload"] == 2
    assert target["adj_validation"] == 1
    assert target["adj_report"] == 0
    assert target["adj_export"] == 0
    assert target["complexity_score"] == "8.7"
    assert target["est_hours_total"] == 23
    assert target["risk_flags"] == "heavy_sql|file_uploads"


def test_reporting_adjustment(generated_output):
    records, _ = generated_output
    target = next(r for r in records if r["process_id"] == "7712b17c939e9e87a10dd676746461b0b004d4d5")
    assert target["adj_report"] == 4
    assert target["risk_flags"] == "reporting"
    assert target["est_hours_total"] == 16
