import csv
import hashlib
import os
import re
from typing import List

IGNORE_DIRS = {
    "src",
    "source",
    "app",
    "apps",
    "web",
    "website",
    "webapp",
    "pages",
    "ui",
    "views",
    "app_code",
    "bin",
    "obj",
    "content",
    "assets",
    "scripts",
    "styles",
}

VERB_PATTERNS = [
    ("EXPORT", re.compile(r"(export|excel|csv|xlsx|download|descargar)", re.IGNORECASE)),
    ("REPORT", re.compile(r"(report|reporte|print|imprimir|rpt)", re.IGNORECASE)),
    ("CREATE", re.compile(r"(create|nuevo|alta|add|insert|register|registrar|crear)", re.IGNORECASE)),
    ("EDIT", re.compile(r"(edit|editar|update|modificar|actualizar)", re.IGNORECASE)),
    ("DETAIL", re.compile(r"(detail|detalle|view|ver)", re.IGNORECASE)),
    ("LIST", re.compile(r"(list|listar|index|browse|grid|consulta|search|buscar)", re.IGNORECASE)),
]


def first_significant_directory(path_parts: List[str]) -> str:
    for part in path_parts:
        if not part:
            continue
        if part.lower() in IGNORE_DIRS:
            continue
        return part
    return ""


def detect_flags(abs_path: str, has_codebehind: bool) -> str:
    flags: List[str] = []
    if has_codebehind:
        flags.append("has_codebehind")

    possible_master = False
    uses_usercontrol = False

    try:
        with open(abs_path, "r", encoding="utf-8", errors="ignore") as fh:
            content = fh.read()
    except FileNotFoundError:
        content = ""

    if "MasterPageFile" in content or re.search(r"<%@\s*Master", content):
        possible_master = True

    if re.search(r"<%@\s*Register", content) or ".ascx" in content.lower():
        uses_usercontrol = True

    if possible_master:
        flags.append("possible_master")
    if uses_usercontrol:
        flags.append("uses_usercontrol")

    return "|".join(flags)


def guess_verb(text: str) -> str:
    for verb, pattern in VERB_PATTERNS:
        if pattern.search(text):
            return verb
    return "LIST"


def parse_bool(value: str) -> bool:
    if value is None:
        return False
    value = value.strip().lower()
    return value in {"1", "true", "yes"}


def main() -> None:
    records = []

    with open("modules_map_full.csv", newline="", encoding="utf-8") as fh:
        reader = csv.DictReader(fh)
        for row in reader:
            if row.get("is_page_aspx", "0").strip() != "1":
                continue

            path_aspx = row["path_rel"].strip()
            process_id = hashlib.sha1(path_aspx.lower().encode("utf-8")).hexdigest()
            parts = path_aspx.split("/")
            module_guess = first_significant_directory(parts[:-1])
            file_name = row.get("file", "")
            page_name, _ = os.path.splitext(file_name)

            text_for_verb = path_aspx.lower()
            verb = guess_verb(text_for_verb)

            codebehind_path = row.get("codebehind_path", "").strip()
            has_codebehind = parse_bool(row.get("has_codebehind", "0"))

            abs_path = os.path.join(path_aspx)
            flags = detect_flags(abs_path, has_codebehind)

            records.append(
                {
                    "process_id": process_id,
                    "module_guess": module_guess,
                    "page_name": page_name,
                    "verb": verb,
                    "path_aspx": path_aspx,
                    "path_vb": codebehind_path,
                    "flags": flags,
                }
            )

    with open("process_catalog_full.csv", "w", newline="", encoding="utf-8") as fh:
        writer = csv.DictWriter(
            fh,
            fieldnames=[
                "process_id",
                "module_guess",
                "page_name",
                "verb",
                "path_aspx",
                "path_vb",
                "flags",
            ],
        )
        writer.writeheader()
        writer.writerows(records)


if __name__ == "__main__":
    main()
