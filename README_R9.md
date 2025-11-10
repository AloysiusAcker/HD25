# R9 — Recetas de estimación

Este paquete genera las recetas de esfuerzo para cada proceso combinando el catálogo completo y las señales de complejidad.

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
