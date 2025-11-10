# R9 — Recetas de estimación

Este paquete genera las recetas de esfuerzo para cada proceso combinando el catálogo completo y las señales de complejidad.

> **Canon:** el archivo canónico de recetas se encuentra en `HD25/data/out/process_recipes_full.csv`. El paquete más reciente en `HD25/out_repo_mapping/<timestamp>` mantiene una copia sincronizada para distribución.

## Ejecución

```bash
python scripts/generate_process_recipes_full.py \
  --catalog process_catalog_full.csv \
  --signals complexity_metrics.csv \
  --out data/out/process_recipes_full.csv
```

El script crea el archivo de salida y la carpeta `data/out` si no existe.

## Pruebas

```bash
pytest -q tests/test_r9_recipes.py
```
