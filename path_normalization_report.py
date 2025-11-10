import csv
import os
import re
import unicodedata
from collections import defaultdict

INPUT_CSV = "modules_map_full.csv"
OUTPUT_CSV = "path_normalization_report.csv"


def normalize_path(path: str) -> str:
    # Replace Windows separators with forward slashes
    replaced = path.replace("\\", "/")
    # Trim leading/trailing whitespace
    trimmed = replaced.strip()
    # Collapse multiple consecutive slashes
    collapsed = re.sub(r"/+", "/", trimmed)
    # Lowercase the result
    lowered = collapsed.lower()
    return lowered


def detect_trailing_space(path: str) -> bool:
    return path != path.strip()


def detect_double_slash(path: str) -> bool:
    return "//" in path.replace("\\", "/")


def detect_unicode_variant(path: str) -> bool:
    return path != unicodedata.normalize("NFKC", path)


def main() -> None:
    if not os.path.exists(INPUT_CSV):
        raise FileNotFoundError(f"Input file '{INPUT_CSV}' not found.")

    with open(INPUT_CSV, newline="", encoding="utf-8") as fh:
        reader = csv.DictReader(fh)
        entries = []
        for row in reader:
            original = row.get("path_rel", "")
            if original is None:
                original = ""
            path_norm = normalize_path(original)
            entry = {
                "path_rel_original": original,
                "path_norm": path_norm,
                "issues": set(),
            }
            if detect_trailing_space(original):
                entry["issues"].add("trailing_space")
            if detect_double_slash(original):
                entry["issues"].add("double_slash")
            if detect_unicode_variant(original):
                entry["issues"].add("unicode_variant")
            entries.append(entry)

    issue_map = {}

    # Assign direct issues first (trailing_space, double_slash, unicode_variant)
    priority_order = ["trailing_space", "double_slash", "unicode_variant"]
    for entry in entries:
        for issue in priority_order:
            if issue in entry["issues"]:
                issue_map.setdefault(entry["path_rel_original"], issue)
                break

    # Detect case collisions for entries not already flagged
    norm_groups = defaultdict(list)
    for entry in entries:
        if entry["path_rel_original"] in issue_map:
            continue
        norm_groups[entry["path_norm"]].append(entry)

    for group in norm_groups.values():
        originals = [e["path_rel_original"] for e in group]
        unique_originals = list(dict.fromkeys(originals))
        if len(unique_originals) <= 1:
            continue
        lower_set = {orig.lower() for orig in unique_originals}
        if len(lower_set) == 1:
            for entry in group:
                issue_map.setdefault(entry["path_rel_original"], "case_collision")

    # Prepare rows without repeating the same original path
    seen = set()
    report_rows = []
    for entry in entries:
        original = entry["path_rel_original"]
        if original in seen:
            continue
        issue = issue_map.get(original)
        if not issue:
            continue
        seen.add(original)
        report_rows.append({
            "path_rel_original": original,
            "path_norm": entry["path_norm"],
            "issue": issue,
        })

    # Sort rows for determinism
    report_rows.sort(key=lambda r: r["path_rel_original"].lower())

    with open(OUTPUT_CSV, "w", newline="", encoding="utf-8") as fh:
        writer = csv.DictWriter(
            fh, fieldnames=["path_rel_original", "path_norm", "issue"], lineterminator="\n"
        )
        writer.writeheader()
        writer.writerows(report_rows)


if __name__ == "__main__":
    main()
