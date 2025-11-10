#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(git rev-parse --show-toplevel)"
PROJECT_DIR="$ROOT_DIR/HD25"
if [ ! -d "$PROJECT_DIR" ]; then
  PROJECT_DIR="$ROOT_DIR"
fi
cd "$PROJECT_DIR"
if [ ! -d out_repo_mapping ]; then
  cd "$ROOT_DIR"
fi

# 1) Detectar paquete más reciente
PKG_DIR="$(ls -td out_repo_mapping/20* | head -n1 || true)"
[ -z "${PKG_DIR:-}" ] && { echo "No hay paquetes en out_repo_mapping"; exit 1; }
echo "Paquete detectado: $PKG_DIR"

# 2) Crear papelera
TRASH_DIR="_trash/$(date +%Y%m%d_%H%M%S)"
mkdir -p "$TRASH_DIR"

# 3) Canon del R9: data/out/process_recipes_full.csv
mkdir -p data/out
if [ -f "$PKG_DIR/process_recipes_full.csv" ]; then
  # Si no existe el canon, copiar desde el paquete
  if [ ! -f "data/out/process_recipes_full.csv" ]; then
    cp -f "$PKG_DIR/process_recipes_full.csv" "data/out/process_recipes_full.csv"
    echo "[INFO] Copiado canon R9 desde paquete."
  fi
  # Verificar consistencia de hash
  H1="$(sha256sum data/out/process_recipes_full.csv | cut -d' ' -f1)"
  H2="$(sha256sum "$PKG_DIR/process_recipes_full.csv" | cut -d' ' -f1)"
  if [ "$H1" != "$H2" ]; then
    echo "[WARN] process_recipes_full.csv difiere entre canon y paquete. Sobrescribiendo paquete con canon."
    cp -f "data/out/process_recipes_full.csv" "$PKG_DIR/process_recipes_full.csv"
  fi
fi

# 4) Archivos que deben vivir SOLO en el paquete (eliminar duplicados en raíz si son idénticos)
PKG_ONLY_FILES=(
  assets_map.csv complexity_metrics.csv config_refs_full.csv coverage_report.csv
  deps_licenses_full.csv endpoints_map.csv integrity_issues.csv js_libs_detected.csv
  modules_map_full.csv path_normalization_report.csv process_catalog_full.csv
  validation_report.md view_costeo_modulos.csv view_costeo_procesos.csv
)

for f in "${PKG_ONLY_FILES[@]}"; do
  if [ -f "$f" ] && [ -f "$PKG_DIR/$f" ]; then
    HR="$(sha256sum "$f" | cut -d' ' -f1)"
    HP="$(sha256sum "$PKG_DIR/$f" | cut -d' ' -f1)"
    if [ "$HR" = "$HP" ]; then
      echo "[DEL] Duplicado idéntico en raíz: $f"
      git rm -f -- "$f"
    else
      echo "[MOVE] Conflicto de contenido para $f → $TRASH_DIR/$f"
      mkdir -p "$TRASH_DIR"
      git mv -f "$f" "$TRASH_DIR/$f"
    fi
  fi
done

# 5) Renombrar vistas “delgadas” a *_summary.csv (en raíz y paquete si existen)
rename_summary () {
  local base="$1"
  local from="$2"
  local to="$3"
  if [ -f "$base/$from" ]; then
    echo "[REN] $base/$from → $base/$to"
    git mv -f "$base/$from" "$base/$to"
  fi
}
rename_summary "."    "deps_licenses.csv" "deps_licenses_summary.csv"
rename_summary "."    "config_refs.csv"   "config_refs_summary.csv"
rename_summary "."    "modules_map.csv"   "modules_map_summary.csv"
rename_summary "$PKG_DIR" "deps_licenses.csv" "deps_licenses_summary.csv" || true
rename_summary "$PKG_DIR" "config_refs.csv"   "config_refs_summary.csv"   || true
rename_summary "$PKG_DIR" "modules_map.csv"   "modules_map_summary.csv"   || true

# 6) Manifest de hashes del paquete
(
  cd "$PKG_DIR"
  sha256sum *.csv *.md 2>/dev/null | sort > HASHES_SHA256.txt || true
)
git add "$PKG_DIR/HASHES_SHA256.txt"

echo "[OK] Limpieza y canon aplicados."
