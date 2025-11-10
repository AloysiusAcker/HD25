#!/usr/bin/env python3
"""Generate UI/code complexity metrics for WebGestor project files.

The script walks the repository and inspects *.aspx, *.ascx, *.master, and
*.vb files. For each file it measures several heuristic indicators of
complexity and writes the aggregated data to ``complexity_metrics.csv``.

The output format is:

    path_rel,loc,n_grid_like,n_form_controls,n_file_uploads,
    n_reports,n_devexpress,n_sql_calls_hint

All counts are non-negative integers and ``loc`` reflects the total number of
lines in the file.  ``path_rel`` is the path relative to the repository root.
"""

from __future__ import annotations

import csv
import os
import re
from dataclasses import dataclass
from typing import Dict, Iterable, Tuple


REPO_ROOT = os.path.dirname(os.path.abspath(__file__))


# Compiled regex patterns for the different metrics. All lookups are performed
# case-insensitively so that variations in casing do not affect the counts.
PATTERNS: Dict[str, re.Pattern[str]] = {
    "n_grid_like": re.compile(r"GridView|DataGrid|Repeater", re.IGNORECASE),
    "n_form_controls": re.compile(
        r"TextBox|DropDownList|CheckBox|RadioButton|Validator",
        re.IGNORECASE,
    ),
    "n_file_uploads": re.compile(
        r"FileUpload|<input[^>]*type\s*=\s*[\"']file[\"']",
        re.IGNORECASE,
    ),
    "n_reports": re.compile(r"ReportViewer|CrystalReport|StiReport|rpt", re.IGNORECASE),
    "n_devexpress": re.compile(r"DevExpress|dx:", re.IGNORECASE),
}


# SQL detection is only applicable to VB files, hence handled separately.
SQL_PATTERN = re.compile(
    r"SqlCommand|OleDbCommand|\bSELECT\b|\bINSERT\b|\bUPDATE\b|\bDELETE\b",
    re.IGNORECASE,
)


TARGET_EXTENSIONS = {".aspx", ".ascx", ".master", ".vb"}


@dataclass
class Metrics:
    """Container for the metrics collected per file."""

    path_rel: str
    loc: int
    n_grid_like: int
    n_form_controls: int
    n_file_uploads: int
    n_reports: int
    n_devexpress: int
    n_sql_calls_hint: int

    @classmethod
    def from_content(cls, path_rel: str, content: str, extension: str) -> "Metrics":
        loc = content.count("\n") + 1 if content else 0
        counts: Dict[str, int] = {name: 0 for name in PATTERNS}

        for name, pattern in PATTERNS.items():
            counts[name] = len(pattern.findall(content))

        sql_count = len(SQL_PATTERN.findall(content)) if extension == ".vb" else 0

        return cls(
            path_rel=path_rel,
            loc=loc,
            n_grid_like=counts["n_grid_like"],
            n_form_controls=counts["n_form_controls"],
            n_file_uploads=counts["n_file_uploads"],
            n_reports=counts["n_reports"],
            n_devexpress=counts["n_devexpress"],
            n_sql_calls_hint=sql_count,
        )


def iter_target_files(root: str) -> Iterable[Tuple[str, str]]:
    """Yield (absolute_path, relative_path) for files with target extensions."""

    for dirpath, _, filenames in os.walk(root):
        for name in filenames:
            _, ext = os.path.splitext(name)
            if ext.lower() in TARGET_EXTENSIONS:
                abs_path = os.path.join(dirpath, name)
                rel_path = os.path.relpath(abs_path, root)
                yield abs_path, rel_path


def read_file(path: str) -> str:
    """Read file content as text using UTF-8 with fallback error handling."""

    with open(path, "r", encoding="utf-8", errors="ignore") as f:
        return f.read()


def compute_metrics() -> Iterable[Metrics]:
    for abs_path, rel_path in sorted(iter_target_files(REPO_ROOT), key=lambda t: t[1]):
        content = read_file(abs_path)
        _, ext = os.path.splitext(abs_path)
        yield Metrics.from_content(rel_path, content, ext.lower())


def write_csv(metrics: Iterable[Metrics], output_path: str) -> None:
    fieldnames = [
        "path_rel",
        "loc",
        "n_grid_like",
        "n_form_controls",
        "n_file_uploads",
        "n_reports",
        "n_devexpress",
        "n_sql_calls_hint",
    ]
    with open(output_path, "w", newline="", encoding="utf-8") as csvfile:
        writer = csv.DictWriter(csvfile, fieldnames=fieldnames)
        writer.writeheader()
        for item in metrics:
            writer.writerow(item.__dict__)


def main() -> None:
    metrics = list(compute_metrics())
    output_csv = os.path.join(REPO_ROOT, "complexity_metrics.csv")
    write_csv(metrics, output_csv)


if __name__ == "__main__":
    main()
