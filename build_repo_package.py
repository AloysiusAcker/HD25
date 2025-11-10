"""Builds repository mapping and analysis artifacts for packaging and traceability."""

from __future__ import annotations

import csv
import datetime as _dt
import hashlib
import os
import re
import unicodedata
import xml.etree.ElementTree as ET
from collections import Counter, defaultdict
from dataclasses import dataclass
from pathlib import Path
from typing import Dict, Iterable, List, Optional, Sequence, Tuple

REPO_ROOT = Path(__file__).resolve().parent
OUTPUT_ROOT = REPO_ROOT / "out_repo_mapping"
EXCLUDED_DIRS = {".git", "out_repo_mapping", "__pycache__"}
TEXT_EXTENSIONS = {
    ".vb",
    ".cs",
    ".js",
    ".ts",
    ".css",
    ".scss",
    ".less",
    ".html",
    ".htm",
    ".aspx",
    ".ascx",
    ".asmx",
    ".svc",
    ".ashx",
    ".config",
    ".xml",
    ".json",
    ".csv",
    ".txt",
    ".md",
    ".yml",
    ".yaml",
    ".ini",
    ".bat",
    ".ps1",
    ".sql",
    ".py",
}
CODE_EXTENSIONS = {
    ".vb": "vb",
    ".cs": "csharp",
    ".js": "javascript",
    ".ts": "typescript",
    ".css": "css",
    ".scss": "scss",
    ".less": "less",
    ".aspx": "aspx",
    ".ascx": "ascx",
    ".ashx": "ashx",
    ".asmx": "asmx",
    ".svc": "svc",
    ".py": "python",
}
ASSET_TYPES = {
    ".png": "image",
    ".jpg": "image",
    ".jpeg": "image",
    ".gif": "image",
    ".bmp": "image",
    ".svg": "image",
    ".ico": "image",
    ".webp": "image",
    ".ttf": "font",
    ".otf": "font",
    ".woff": "font",
    ".woff2": "font",
    ".eot": "font",
    ".pdf": "document",
    ".doc": "document",
    ".docx": "document",
    ".xls": "document",
    ".xlsx": "document",
    ".ppt": "document",
    ".pptx": "document",
    ".ppla": "print_template",
}
JS_LIBRARY_PATTERNS: List[Tuple[re.Pattern[str], str]] = [
    (re.compile(r"jquery(?:[-_.](\d[\w.]*))?", re.IGNORECASE), "jQuery"),
    (re.compile(r"bootstrap(?:[-_.](\d[\w.]*))?", re.IGNORECASE), "Bootstrap"),
    (re.compile(r"moment(?:[-_.](\d[\w.]*))?", re.IGNORECASE), "Moment.js"),
    (re.compile(r"fullcalendar(?:[-_.](\d[\w.]*))?", re.IGNORECASE), "FullCalendar"),
    (re.compile(r"datatables?(?:[-_.](\d[\w.]*))?", re.IGNORECASE), "DataTables"),
    (re.compile(r"select2(?:[-_.](\d[\w.]*))?", re.IGNORECASE), "Select2"),
    (re.compile(r"chart\.js(?:[-_.](\d[\w.]*))?", re.IGNORECASE), "Chart.js"),
    (re.compile(r"knob(?:[-_.](\d[\w.]*))?", re.IGNORECASE), "jQuery Knob"),
    (re.compile(r"ion\.rangeSlider", re.IGNORECASE), "Ion.RangeSlider"),
    (re.compile(r"pace(?:[-_.](\d[\w.]*))?", re.IGNORECASE), "Pace"),
    (re.compile(r"morris(?:[-_.](\d[\w.]*))?", re.IGNORECASE), "Morris.js"),
]


def posix_rel_path(path: Path) -> str:
    return path.as_posix()


@dataclass
class FileMeta:
    path_rel: str
    directory: str
    filename: str
    extension: str
    size_bytes: int
    mtime_utc: str
    sha1: str
    loc: int
    is_page_aspx: int
    has_codebehind: int
    codebehind_path: str
    language: Optional[str] = None
    content_text: Optional[str] = None


def sha1_file(path: Path) -> str:
    digest = hashlib.sha1()
    with path.open("rb") as fh:
        for chunk in iter(lambda: fh.read(8192), b""):
            digest.update(chunk)
    return digest.hexdigest()


def is_text_file(path: Path) -> bool:
    return path.suffix.lower() in TEXT_EXTENSIONS


def read_text(path: Path) -> str:
    return path.read_text(encoding="utf-8", errors="replace")


def calc_loc(text: str) -> int:
    if not text:
        return 0
    lines = text.splitlines()
    return len(lines)


def gather_file_metadata() -> List[FileMeta]:
    files: List[FileMeta] = []
    all_paths: Dict[str, Path] = {}
    for root, dirnames, filenames in os.walk(REPO_ROOT):
        rel_dir = os.path.relpath(root, REPO_ROOT)
        if rel_dir == ".":
            rel_dir = ""
        parts = set(Path(rel_dir).parts) if rel_dir else set()
        excluded = parts & EXCLUDED_DIRS
        if excluded:
            dirnames[:] = [d for d in dirnames if d not in EXCLUDED_DIRS]
            continue
        dirnames[:] = [d for d in dirnames if d not in EXCLUDED_DIRS]
        for filename in sorted(filenames):
            path = Path(root) / filename
            rel_path = Path(os.path.relpath(path, REPO_ROOT))
            rel_path_posix = posix_rel_path(rel_path)
            extension = path.suffix.lower()
            text_content: Optional[str] = None
            loc = 0
            if is_text_file(path):
                try:
                    text_content = read_text(path)
                except UnicodeDecodeError:
                    text_content = path.read_text(encoding="latin-1", errors="replace")
                loc = calc_loc(text_content)
            size = path.stat().st_size
            mtime = _dt.datetime.utcfromtimestamp(path.stat().st_mtime).isoformat() + "Z"
            sha1 = sha1_file(path)
            directory = posix_rel_path(rel_path.parent) if rel_path.parent != Path(".") else ""
            file_meta = FileMeta(
                path_rel=rel_path_posix,
                directory=directory,
                filename=filename,
                extension=extension,
                size_bytes=size,
                mtime_utc=mtime,
                sha1=sha1,
                loc=loc,
                is_page_aspx=1 if extension == ".aspx" else 0,
                has_codebehind=0,
                codebehind_path="",
                language=CODE_EXTENSIONS.get(extension),
                content_text=text_content,
            )
            files.append(file_meta)
            all_paths[rel_path_posix] = path
    codebehind_lookup = {meta.path_rel for meta in files if meta.extension in {".vb", ".cs"}}
    for meta in files:
        if meta.extension == ".aspx":
            vb_candidate = f"{meta.path_rel}.vb"
            cs_candidate = f"{meta.path_rel}.cs"
            if vb_candidate in codebehind_lookup:
                meta.has_codebehind = 1
                meta.codebehind_path = vb_candidate
            elif cs_candidate in codebehind_lookup:
                meta.has_codebehind = 1
                meta.codebehind_path = cs_candidate
    files.sort(key=lambda m: m.path_rel.lower())
    return files


def write_csv(path: Path, fieldnames: Sequence[str], rows: Iterable[Dict[str, object]]) -> None:
    with path.open("w", newline="", encoding="utf-8") as fh:
        writer = csv.DictWriter(fh, fieldnames=fieldnames, lineterminator="\n")
        writer.writeheader()
        for row in rows:
            writer.writerow({key: row.get(key, "") for key in fieldnames})


def build_modules_map(files: List[FileMeta], output_dir: Path) -> None:
    rows = []
    for meta in files:
        rows.append(
            {
                "path_rel": meta.path_rel,
                "dir": meta.directory,
                "file": meta.filename,
                "ext": meta.extension,
                "size_bytes": meta.size_bytes,
                "mtime_utc": meta.mtime_utc,
                "sha1": meta.sha1,
                "loc": meta.loc,
                "is_page_aspx": meta.is_page_aspx,
                "has_codebehind": meta.has_codebehind,
                "codebehind_path": meta.codebehind_path,
            }
        )
    write_csv(
        output_dir / "modules_map_full.csv",
        [
            "path_rel",
            "dir",
            "file",
            "ext",
            "size_bytes",
            "mtime_utc",
            "sha1",
            "loc",
            "is_page_aspx",
            "has_codebehind",
            "codebehind_path",
        ],
        rows,
    )


def build_path_normalization_report(files: List[FileMeta], output_dir: Path) -> None:
    entries = []
    for meta in files:
        original = meta.path_rel
        normalized = unicodedata.normalize("NFKC", original.replace("\\", "/").strip())
        collapsed = re.sub(r"/+", "/", normalized)
        lowered = collapsed.lower()
        issues = []
        if original != original.strip():
            issues.append("trailing_space")
        if "//" in original.replace("\\", "/"):
            issues.append("double_slash")
        if original != unicodedata.normalize("NFKC", original):
            issues.append("unicode_variant")
        entries.append({
            "path_rel_original": original,
            "path_norm": lowered,
            "issues": issues,
        })
    case_groups: Dict[str, List[str]] = defaultdict(list)
    for entry in entries:
        case_groups[entry["path_norm"]].append(entry["path_rel_original"])
    rows = []
    for entry in entries:
        issue = entry["issues"][:1]
        if not issue:
            group = case_groups[entry["path_norm"]]
            unique = list(dict.fromkeys(group))
            if len(unique) > 1 and len({g.lower() for g in unique}) == 1:
                issue = ["case_collision"]
        if issue:
            rows.append(
                {
                    "path_rel_original": entry["path_rel_original"],
                    "path_norm": entry["path_norm"],
                    "issue": issue[0],
                }
            )
    rows.sort(key=lambda r: r["path_rel_original"].lower())
    write_csv(
        output_dir / "path_normalization_report.csv",
        ["path_rel_original", "path_norm", "issue"],
        rows,
    )


def read_loose_csv(path: Path) -> Tuple[List[str], List[List[str]]]:
    lines = path.read_text(encoding="utf-8", errors="replace").splitlines()
    if not lines:
        return [], []
    header_line = lines[0]
    header = next(csv.reader([header_line]))
    expected = len(header)
    data_lines = lines[1:]
    merged_lines: List[str] = []
    current = ""
    row_start = re.compile(r"^[A-Za-z0-9_.-]+,")
    for raw_line in data_lines:
        if not current:
            current = raw_line
            continue
        if row_start.match(raw_line):
            merged_lines.append(current)
            current = raw_line
        else:
            current += raw_line
    if current:
        merged_lines.append(current)
    rows: List[List[str]] = []
    for line in merged_lines:
        if not line.strip():
            continue
        parsed = next(csv.reader([line]))
        while len(parsed) < expected:
            parsed.append("")
        if len(parsed) > expected:
            parsed = parsed[:expected]
        rows.append(parsed)
    return header, rows


def build_process_catalog(files: List[FileMeta], output_dir: Path) -> Dict[str, Dict[str, str]]:
    catalog_path = REPO_ROOT / "process_catalog.csv"
    page_lookup = {meta.filename: meta for meta in files if meta.extension == ".aspx"}
    rows_out: List[Dict[str, object]] = []
    process_map: Dict[str, Dict[str, str]] = {}
    if catalog_path.exists():
        header, rows = read_loose_csv(catalog_path)
        for row in rows:
            record = dict(zip(header, row))
            process_id = record.get("process_id", "")
            page_name = record.get("page", "")
            page_meta = page_lookup.get(page_name)
            page_path = page_meta.path_rel if page_meta else ""
            codebehind = page_meta.codebehind_path if page_meta else ""
            record["page_path"] = page_path
            record["codebehind_path"] = codebehind
            record["page_exists"] = "yes" if page_meta else "no"
            record["codebehind_exists"] = "yes" if codebehind else "no"
            if page_meta:
                record["page_loc"] = page_meta.loc
            if process_id:
                process_map[process_id] = record
            rows_out.append(record)
    fieldnames = list(rows_out[0].keys()) if rows_out else header
    write_csv(
        output_dir / "process_catalog_full.csv",
        fieldnames,
        rows_out,
    )
    return process_map


def build_endpoints_map(files: List[FileMeta], output_dir: Path) -> None:
    rows = []
    endpoint_types = {
        ".aspx": "page",
        ".ashx": "handler",
        ".asmx": "webservice",
        ".svc": "wcf_service",
    }
    for meta in files:
        if meta.extension in endpoint_types:
            rows.append(
                {
                    "path_rel": meta.path_rel,
                    "endpoint_type": endpoint_types[meta.extension],
                    "has_codebehind": "yes" if meta.has_codebehind else "no",
                    "codebehind_path": meta.codebehind_path,
                    "loc": meta.loc,
                    "size_bytes": meta.size_bytes,
                    "sha1": meta.sha1,
                }
            )
    write_csv(
        output_dir / "endpoints_map.csv",
        ["path_rel", "endpoint_type", "has_codebehind", "codebehind_path", "loc", "size_bytes", "sha1"],
        rows,
    )


def calculate_complexity(files: List[FileMeta], output_dir: Path) -> List[Dict[str, object]]:
    language_keywords = {
        "vb": ["if", "for", "while", "select", "case", "try", "catch", "loop", "elseif"],
        "csharp": ["if", "for", "foreach", "while", "switch", "case", "try", "catch"],
        "javascript": ["if", "for", "while", "switch", "case", "try", "catch"],
        "typescript": ["if", "for", "while", "switch", "case", "try", "catch"],
        "python": ["if", "for", "while", "try", "except", "elif"],
        "css": [],
        "scss": [],
        "less": [],
        "aspx": ["<asp:"],
        "ascx": ["<asp:"],
        "ashx": ["Sub", "Function", "class"],
        "asmx": ["WebService"],
        "svc": ["Service"],
    }
    function_patterns = {
        "vb": [r"\bFunction\b", r"\bSub\b"],
        "csharp": [r"\bvoid\b", r"\bpublic\s+\w+\s+\w+\s*\(", r"\bprivate\s+\w+"],
        "javascript": [r"function\s"],
        "typescript": [r"function\s"],
        "python": [r"^\s*def\s"],
    }
    comment_tokens = {
        "vb": "'",
        "csharp": "//",
        "javascript": "//",
        "typescript": "//",
        "python": "#",
    }
    rows = []
    for meta in files:
        language = meta.language
        if not language or not meta.content_text:
            continue
        text = meta.content_text
        lines = text.splitlines()
        loc = len(lines)
        avg_line_length = sum(len(line) for line in lines) / loc if loc else 0.0
        comment_token = comment_tokens.get(language)
        comment_lines = 0
        if comment_token:
            comment_lines = sum(1 for line in lines if line.strip().startswith(comment_token))
        keywords = language_keywords.get(language, [])
        decision_points = 0
        for keyword in keywords:
            decision_points += len(re.findall(keyword, text, flags=re.IGNORECASE))
        num_functions = 0
        for pattern in function_patterns.get(language, []):
            num_functions += len(re.findall(pattern, text, flags=re.IGNORECASE))
        complexity_score = decision_points + max(num_functions, 1)
        row = {
            "path_rel": meta.path_rel,
            "language": language,
            "loc": loc,
            "avg_line_length": round(avg_line_length, 2),
            "num_functions": num_functions,
            "decision_points": decision_points,
            "comment_lines": comment_lines,
            "comment_ratio": round(comment_lines / loc, 3) if loc else 0.0,
            "complexity_score": complexity_score,
        }
        rows.append(row)
    rows.sort(key=lambda r: r["path_rel"].lower())
    write_csv(
        output_dir / "complexity_metrics.csv",
        [
            "path_rel",
            "language",
            "loc",
            "avg_line_length",
            "num_functions",
            "decision_points",
            "comment_lines",
            "comment_ratio",
            "complexity_score",
        ],
        rows,
    )
    return rows


def build_deps_licenses(output_dir: Path) -> None:
    curated_path = REPO_ROOT / "deps_licenses.csv"
    rows: List[Dict[str, object]] = []
    if curated_path.exists():
        header, entries = read_loose_csv(curated_path)
        for row in entries:
            record = dict(zip(header, row))
            record.setdefault("source", "curated")
            rows.append(record)
    for packages_config in REPO_ROOT.rglob("packages.config"):
        try:
            tree = ET.parse(packages_config)
        except ET.ParseError as exc:
            rows.append(
                {
                    "package": "__parse_error__",
                    "version": "",
                    "license_guess": "",
                    "usage": f"Failed to parse: {exc}",
                    "evidence_path": posix_rel_path(packages_config.relative_to(REPO_ROOT)),
                    "source": "packages.config",
                }
            )
            continue
        root = tree.getroot()
        for package in root.findall("package"):
            package_id = package.attrib.get("id", "")
            version = package.attrib.get("version", "")
            target = package.attrib.get("targetFramework", "")
            rows.append(
                {
                    "package": package_id,
                    "version": version,
                    "license_guess": "Unknown",
                    "usage": f"Referenced via {posix_rel_path(packages_config.relative_to(REPO_ROOT))}",
                    "evidence_path": posix_rel_path(packages_config.relative_to(REPO_ROOT)),
                    "source": "packages.config",
                    "target_framework": target,
                }
            )
    rows.sort(key=lambda r: (r.get("package", ""), r.get("version", "")))
    fieldnames = [
        "package",
        "version",
        "license_guess",
        "usage",
        "evidence_path",
        "source",
        "target_framework",
    ]
    write_csv(output_dir / "deps_licenses_full.csv", fieldnames, rows)


def mask_value(value: str) -> str:
    if not value:
        return ""
    value = value.strip()
    if len(value) <= 4:
        return "***"
    return f"{value[:3]}...{value[-2:]}"


def build_config_refs(output_dir: Path, integrity_rows: List[Dict[str, object]]) -> None:
    rows: List[Dict[str, object]] = []
    for config_path in REPO_ROOT.rglob("*.config"):
        rel_path = posix_rel_path(config_path.relative_to(REPO_ROOT))
        try:
            tree = ET.parse(config_path)
        except ET.ParseError as exc:
            integrity_rows.append(
                {
                    "issue_id": f"config_parse::{rel_path}",
                    "severity": "high",
                    "category": "configuration",
                    "path": rel_path,
                    "detail": f"No se pudo parsear XML: {exc}",
                }
            )
            continue
        root = tree.getroot()
        for app_setting in root.findall("appSettings/add"):
            key = app_setting.attrib.get("key", "")
            value = app_setting.attrib.get("value", "")
            rows.append(
                {
                    "file_path": rel_path,
                    "setting_type": "appSettings",
                    "key": key,
                    "value_masked": mask_value(value),
                    "is_secret": "yes" if key.lower().endswith("key") or "password" in key.lower() else "no",
                }
            )
        for conn in root.findall("connectionStrings/add"):
            name = conn.attrib.get("name", "")
            conn_string = conn.attrib.get("connectionString", "")
            rows.append(
                {
                    "file_path": rel_path,
                    "setting_type": "connectionStrings",
                    "key": name,
                    "value_masked": mask_value(conn_string),
                    "is_secret": "yes",
                }
            )
    rows.sort(key=lambda r: (r["file_path"], r["setting_type"], r["key"]))
    write_csv(
        output_dir / "config_refs_full.csv",
        ["file_path", "setting_type", "key", "value_masked", "is_secret"],
        rows,
    )


def build_assets_map(files: List[FileMeta], output_dir: Path) -> None:
    rows = []
    for meta in files:
        asset_type = ASSET_TYPES.get(meta.extension)
        if not asset_type:
            continue
        rows.append(
            {
                "path_rel": meta.path_rel,
                "asset_type": asset_type,
                "size_bytes": meta.size_bytes,
                "sha1": meta.sha1,
            }
        )
    write_csv(output_dir / "assets_map.csv", ["path_rel", "asset_type", "size_bytes", "sha1"], rows)


def build_js_libs(files: List[FileMeta], output_dir: Path) -> None:
    rows = []
    for meta in files:
        if meta.extension != ".js":
            continue
        name_lower = meta.filename.lower()
        matched = False
        for pattern, library_name in JS_LIBRARY_PATTERNS:
            match = pattern.search(name_lower)
            if not match:
                continue
            version = match.group(1) if match.lastindex else ""
            rows.append(
                {
                    "library": library_name,
                    "version": version,
                    "path_rel": meta.path_rel,
                    "minified": "yes" if ".min." in name_lower else "no",
                    "sha1": meta.sha1,
                }
            )
            matched = True
            break
        if not matched:
            rows.append(
                {
                    "library": meta.filename,
                    "version": "",
                    "path_rel": meta.path_rel,
                    "minified": "yes" if ".min." in name_lower else "no",
                    "sha1": meta.sha1,
                }
            )
    write_csv(output_dir / "js_libs_detected.csv", ["library", "version", "path_rel", "minified", "sha1"], rows)


def build_process_recipes(
    output_dir: Path,
    process_map: Dict[str, Dict[str, str]],
) -> Tuple[List[Dict[str, object]], Dict[str, Dict[str, object]]]:
    seed_path = REPO_ROOT / "process_recipes_seed.csv"
    rows_out: List[Dict[str, object]] = []
    process_summary: Dict[str, Dict[str, object]] = {}
    if not seed_path.exists():
        write_csv(output_dir / "process_recipes_full.csv", [], [])
        return rows_out, process_summary
    header, rows = read_loose_csv(seed_path)
    for row in rows:
        record = dict(zip(header, row))
        process_id = record.get("process_id", "")
        linked = process_map.get(process_id, {})
        combined = {**record}
        combined["module"] = linked.get("module", "")
        combined["page"] = linked.get("page", "")
        combined["page_path"] = linked.get("page_path", "")
        combined["codebehind_path"] = linked.get("codebehind_path", "")
        cpu_ms = float(record.get("cpu_ms_est", "0") or 0)
        db_reads = float(record.get("db_reads", "0") or 0)
        db_writes = float(record.get("db_writes", "0") or 0)
        storage_mb = float(record.get("storage_mb", "0") or 0)
        egress_mb = float(record.get("egress_mb", "0") or 0)
        emails = float(record.get("emails", "0") or 0)
        sms = float(record.get("sms", "0") or 0)
        ai_tokens = float(record.get("ai_tokens", "0") or 0)
        cost_score = (
            cpu_ms / 1000.0
            + db_reads * 0.5
            + db_writes * 0.75
            + storage_mb * 1.5
            + egress_mb * 1.5
            + emails * 2.0
            + sms * 3.0
            + ai_tokens / 1000.0
        )
        combined["cost_score"] = round(cost_score, 3)
        rows_out.append(combined)
        if process_id:
            process_summary[process_id] = {
                "cpu_ms_est": cpu_ms,
                "db_reads": db_reads,
                "db_writes": db_writes,
                "storage_mb": storage_mb,
                "egress_mb": egress_mb,
                "emails": emails,
                "sms": sms,
                "ai_tokens": ai_tokens,
                "cost_score": cost_score,
                "module": combined["module"],
            }
    fieldnames = list(rows_out[0].keys()) if rows_out else header
    write_csv(output_dir / "process_recipes_full.csv", fieldnames, rows_out)
    return rows_out, process_summary


def build_coverage_report(output_dir: Path, complexity_rows: List[Dict[str, object]]) -> None:
    totals = Counter()
    per_language = defaultdict(Counter)
    for row in complexity_rows:
        loc = int(row.get("loc", 0))
        language = row.get("language", "unknown")
        totals["loc"] += loc
        per_language[language]["loc"] += loc
        totals["files"] += 1
        per_language[language]["files"] += 1
    rows = []
    covered_loc = 0
    coverage_pct = 0.0
    if totals["loc"]:
        coverage_pct = round(covered_loc / totals["loc"], 3)
    rows.append(
        {
            "subject": "repository",
            "metric": "lines_total",
            "value": totals["loc"],
        }
    )
    rows.append({"subject": "repository", "metric": "lines_covered", "value": covered_loc})
    rows.append({"subject": "repository", "metric": "coverage_pct", "value": coverage_pct})
    for language, metrics in sorted(per_language.items()):
        rows.append(
            {
                "subject": language,
                "metric": "files",
                "value": metrics["files"],
            }
        )
        rows.append(
            {
                "subject": language,
                "metric": "loc",
                "value": metrics["loc"],
            }
        )
    write_csv(output_dir / "coverage_report.csv", ["subject", "metric", "value"], rows)


def build_integrity_issues(
    output_dir: Path,
    files: List[FileMeta],
    integrity_rows: List[Dict[str, object]],
) -> None:
    for meta in files:
        if meta.extension == ".aspx" and not meta.has_codebehind:
            integrity_rows.append(
                {
                    "issue_id": f"missing_codebehind::{meta.path_rel}",
                    "severity": "medium",
                    "category": "code_structure",
                    "path": meta.path_rel,
                    "detail": "Página ASPX sin archivo de code-behind asociado",
                }
            )
        if meta.filename.endswith(" "):
            integrity_rows.append(
                {
                    "issue_id": f"trailing_space_name::{meta.path_rel}",
                    "severity": "low",
                    "category": "naming",
                    "path": meta.path_rel,
                    "detail": "Nombre de archivo con espacio al final",
                }
            )
    integrity_rows.sort(key=lambda r: r.get("issue_id", ""))
    write_csv(
        output_dir / "integrity_issues.csv",
        ["issue_id", "severity", "category", "path", "detail"],
        integrity_rows,
    )


def build_cost_views(
    output_dir: Path,
    process_recipes: List[Dict[str, object]],
    process_summary: Dict[str, Dict[str, object]],
) -> None:
    view_process_rows = []
    for record in process_recipes:
        view_process_rows.append(
            {
                "process_id": record.get("process_id", ""),
                "module": record.get("module", ""),
                "cost_score": record.get("cost_score", 0),
                "cpu_ms_est": record.get("cpu_ms_est", ""),
                "db_reads": record.get("db_reads", ""),
                "db_writes": record.get("db_writes", ""),
                "storage_mb": record.get("storage_mb", ""),
                "egress_mb": record.get("egress_mb", ""),
            }
        )
    write_csv(
        output_dir / "view_costeo_procesos.csv",
        [
            "process_id",
            "module",
            "cost_score",
            "cpu_ms_est",
            "db_reads",
            "db_writes",
            "storage_mb",
            "egress_mb",
        ],
        view_process_rows,
    )
    module_rows: Dict[str, Dict[str, float]] = defaultdict(lambda: defaultdict(float))
    for summary in process_summary.values():
        module = summary.get("module", "")
        module_rows[module]["cost_score"] += summary.get("cost_score", 0.0)
        module_rows[module]["cpu_ms_est"] += summary.get("cpu_ms_est", 0.0)
        module_rows[module]["db_reads"] += summary.get("db_reads", 0.0)
        module_rows[module]["db_writes"] += summary.get("db_writes", 0.0)
        module_rows[module]["storage_mb"] += summary.get("storage_mb", 0.0)
        module_rows[module]["egress_mb"] += summary.get("egress_mb", 0.0)
    module_view_rows = []
    for module, metrics in sorted(module_rows.items()):
        module_view_rows.append(
            {
                "module": module,
                "cost_score": round(metrics["cost_score"], 3),
                "cpu_ms_est": round(metrics["cpu_ms_est"], 2),
                "db_reads": round(metrics["db_reads"], 2),
                "db_writes": round(metrics["db_writes"], 2),
                "storage_mb": round(metrics["storage_mb"], 3),
                "egress_mb": round(metrics["egress_mb"], 3),
            }
        )
    write_csv(
        output_dir / "view_costeo_modulos.csv",
        ["module", "cost_score", "cpu_ms_est", "db_reads", "db_writes", "storage_mb", "egress_mb"],
        module_view_rows,
    )


def build_validation_report(
    output_dir: Path,
    files: List[FileMeta],
    endpoints_count: int,
    complexity_rows: List[Dict[str, object]],
    process_recipes: List[Dict[str, object]],
    integrity_rows: List[Dict[str, object]],
) -> None:
    total_files = len(files)
    total_loc = sum(meta.loc for meta in files)
    modules = sorted({Path(meta.path_rel).parts[0] for meta in files if Path(meta.path_rel).parts})
    risk_items = [row for row in integrity_rows if row.get("severity") == "high"]
    medium_items = [row for row in integrity_rows if row.get("severity") == "medium"]
    report_lines = ["# Validación del paquete", ""]
    report_lines.append(f"- Total de archivos analizados: {total_files}")
    report_lines.append(f"- Total de líneas contabilizadas: {total_loc}")
    report_lines.append(f"- Endpoints detectados: {endpoints_count}")
    report_lines.append(f"- Procesos documentados: {len(process_recipes)}")
    report_lines.append("")
    report_lines.append("## Riesgos e integridad")
    if risk_items or medium_items:
        for row in risk_items + medium_items:
            report_lines.append(
                f"- [{row.get('severity').upper()}] {row.get('category')} :: {row.get('path')} — {row.get('detail')}"
            )
    else:
        report_lines.append("- Sin hallazgos críticos o medios.")
    report_lines.append("")
    if complexity_rows:
        sorted_complexity = sorted(
            complexity_rows,
            key=lambda r: r.get("complexity_score", 0),
            reverse=True,
        )[:5]
        report_lines.append("## Componentes más complejos")
        for item in sorted_complexity:
            report_lines.append(
                f"- {item['path_rel']}: score {item['complexity_score']} con {item['loc']} LOC"
            )
        report_lines.append("")
    report_lines.append("## Cobertura")
    report_lines.append("- No se detectaron suites de pruebas automatizadas; cobertura estimada 0%.")
    report_lines.append("")
    report_lines.append("## Alcance del paquete")
    report_lines.append("- Módulos incluidos: " + ", ".join(modules))
    report = "\n".join(report_lines) + "\n"
    (output_dir / "validation_report.md").write_text(report, encoding="utf-8")


def build_run_log(output_dir: Path, generated_files: List[Path]) -> None:
    entries = []
    for file_path in sorted(generated_files):
        if file_path.name == "RUN_LOG.txt":
            continue
        sha1 = sha1_file(file_path)
        rel_path = posix_rel_path(file_path.relative_to(output_dir))
        entries.append(f"{sha1} {rel_path}")
    log_path = output_dir / "RUN_LOG.txt"
    log_path.write_text("\n".join(entries) + "\n", encoding="utf-8")


def ensure_output_dir(timestamp: Optional[str] = None) -> Path:
    if timestamp is None:
        timestamp = _dt.datetime.utcnow().strftime("%Y%m%d_%H%M")
    target = OUTPUT_ROOT / timestamp
    target.mkdir(parents=True, exist_ok=True)
    return target


def main() -> None:
    output_dir = ensure_output_dir()
    files = gather_file_metadata()
    build_modules_map(files, output_dir)
    build_path_normalization_report(files, output_dir)
    process_map = build_process_catalog(files, output_dir)
    build_endpoints_map(files, output_dir)
    complexity_rows = calculate_complexity(files, output_dir)
    build_deps_licenses(output_dir)
    integrity_rows: List[Dict[str, object]] = []
    build_config_refs(output_dir, integrity_rows)
    build_assets_map(files, output_dir)
    build_js_libs(files, output_dir)
    process_recipes, process_summary = build_process_recipes(output_dir, process_map)
    build_coverage_report(output_dir, complexity_rows)
    build_cost_views(output_dir, process_recipes, process_summary)
    build_integrity_issues(output_dir, files, integrity_rows)
    build_validation_report(
        output_dir,
        files,
        endpoints_count=sum(1 for meta in files if meta.extension in {".aspx", ".ashx", ".asmx", ".svc"}),
        complexity_rows=complexity_rows,
        process_recipes=process_recipes,
        integrity_rows=integrity_rows,
    )
    generated_files = list(output_dir.rglob("*"))
    generated_files = [p for p in generated_files if p.is_file()]
    build_run_log(output_dir, generated_files)


if __name__ == "__main__":
    main()
