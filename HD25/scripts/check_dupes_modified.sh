#!/usr/bin/env bash
set -euo pipefail
cd "$(git rev-parse --show-toplevel)"

# Archivos modificados en los últimos 2 días (según git log)
FILES=$(git ls-files -z | xargs -0 -I{} bash -lc \
  'T=$(git log -1 --since="2 days ago" --pretty=%ct -- "{}" 2>/dev/null || echo 0); [ "$T" -gt 0 ] && echo "{}"' )

declare -A seen
FAIL=0
for f in $FILES; do
  H=$(sha256sum "$f" | cut -d' ' -f1)
  if [[ -n "${seen[$H]:-}" ]]; then
    echo "::error ::Duplicado por contenido: $f == ${seen[$H]}"
    FAIL=1
  else
    seen[$H]="$f"
  fi
done

exit $FAIL
