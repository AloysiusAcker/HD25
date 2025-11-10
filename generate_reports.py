import csv
from collections import Counter, defaultdict
from pathlib import Path

ROOT = Path(__file__).parent
modules_map_path = ROOT / 'modules_map.csv'
process_catalog_path = ROOT / 'process_catalog.csv'
config_refs_path = ROOT / 'config_refs.csv'

modules_rows = []
with modules_map_path.open(newline='', encoding='utf-8') as f:
    reader = csv.DictReader(f)
    for row in reader:
        modules_rows.append(row)

existing_paths = {row['path'] for row in modules_rows}

aspx_rows = [row for row in modules_rows if row['ext'] == '.aspx']
aspx_total = len(aspx_rows)

# Map .aspx by (module, file_name)
aspx_keys = set()
codebehind_expectations = []
for row in aspx_rows:
    module = (row.get('module') or '').strip()
    file_name = (row.get('file_name') or '').strip()
    aspx_keys.add((module, file_name))
    has_codebehind = (row.get('has_codebehind') or '').strip().lower()
    if has_codebehind in {'si', 'sí', 'yes', 'true', '1'}:
        codebehind_expectations.append(row)

missing_codebehind = []
referenced_codebehind_paths = set()
for row in codebehind_expectations:
    cb_path = (row.get('codebehind_path') or '').strip()
    if cb_path:
        referenced_codebehind_paths.add(cb_path)
    if not cb_path:
        missing_codebehind.append((row['path'], 'Entry indicates codebehind but no path was provided.'))
    elif cb_path not in existing_paths:
        missing_codebehind.append((row['path'], f"Codebehind '{cb_path}' not found in modules map."))

codebehind_rows = [row for row in modules_rows if row['ext'] == '.aspx.vb']

unreferenced_codebehind = []
for row in codebehind_rows:
    path = row['path']
    if path not in referenced_codebehind_paths:
        unreferenced_codebehind.append(path)

process_rows = []
with process_catalog_path.open(newline='', encoding='utf-8') as f:
    reader = csv.DictReader(f)
    for row in reader:
        process_rows.append(row)

endpoints_total = len(process_rows)
process_ids = [row.get('process_id', '').strip() for row in process_rows if row.get('process_id')]
unique_process_ids = set(process_ids)

page_references = defaultdict(list)
for row in process_rows:
    module = (row.get('module') or '').strip()
    page = (row.get('page') or '').strip()
    if not page:
        continue
    key = (module, page)
    page_references[key].append(row.get('process_id', '').strip())

catalogued_aspx_keys = {key for key in page_references if key in aspx_keys}
aspx_catalogadas = len(catalogued_aspx_keys)

missing_page_refs = {key: refs for key, refs in page_references.items() if key not in aspx_keys}

duplicate_process_ids = {pid: count for pid, count in Counter(process_ids).items() if count > 1}

configs_rows = []
if config_refs_path.exists():
    with config_refs_path.open(newline='', encoding='utf-8') as f:
        reader = csv.DictReader(f)
        for row in reader:
            configs_rows.append(row)
configs_detectadas = len(configs_rows)

pct_catalogo = 0.0
if aspx_total:
    pct_catalogo = (aspx_catalogadas / aspx_total) * 100

coverage_rows = [
    ('aspx_total', str(aspx_total)),
    ('aspx_catalogadas', str(aspx_catalogadas)),
    ('%catalogo', f"{pct_catalogo:.2f}"),
    ('endpoints_total', str(endpoints_total)),
    ('endpoints_unicos', str(len(unique_process_ids))),
    ('pages_referenciadas', str(len(page_references))),
    ('configs_detectadas', str(configs_detectadas)),
    ('endpoints_sin_pagina', str(len(missing_page_refs))),
    ('codebehind_faltantes', str(len(missing_codebehind))),
    ('codebehind_sin_aspx', str(len(unreferenced_codebehind))),
]

with (ROOT / 'coverage_report.csv').open('w', newline='', encoding='utf-8') as f:
    writer = csv.writer(f)
    writer.writerow(['metric', 'value'])
    writer.writerows(coverage_rows)

integrity_rows = []
for path, detail in missing_codebehind:
    integrity_rows.append({'issue_type': 'aspx_without_vb_expected', 'path_rel': path, 'details': detail})

for path in sorted(unreferenced_codebehind):
    integrity_rows.append({
        'issue_type': 'vb_unreferenced',
        'path_rel': path,
        'details': 'Codebehind file is present but not referenced by any .aspx page.'
    })

for (module, page), refs in sorted(missing_page_refs.items()):
    rel = f"{module}/{page}" if module else page
    refs = [r for r in refs if r]
    detail = 'Referenced process IDs: ' + ', '.join(refs[:5])
    if len(refs) > 5:
        detail += f" (and {len(refs) - 5} more)"
    if not refs:
        detail = 'Page referenced without specific process_id information.'
    integrity_rows.append({
        'issue_type': 'endpoint_without_mapping',
        'path_rel': rel,
        'details': detail
    })

for pid, count in sorted(duplicate_process_ids.items()):
    integrity_rows.append({
        'issue_type': 'duplicate_route',
        'path_rel': pid,
        'details': f'Process ID appears {count} times in process_catalog.csv.'
    })

with (ROOT / 'integrity_issues.csv').open('w', newline='', encoding='utf-8') as f:
    writer = csv.DictWriter(f, fieldnames=['issue_type', 'path_rel', 'details'])
    writer.writeheader()
    for row in integrity_rows:
        writer.writerow(row)
