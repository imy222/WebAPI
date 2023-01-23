#!/usr/bin/env bash
set -euo pipefail

imageTag="$(git rev-parse --short origin/pipeline)"
 
die() { echo "$1"; exit "${2:-1}"; }

[[ -z "$imageTag" ]] && die "Image Tag Is Empty" || echo "Valid Image Tag"

echo "Deploy to Jupiter"
ktmpl ./Deployment/template.yaml -f ./Deployment/default.yaml -p imageTag "${imageTag}"  | kubectl apply -f - --validate=false
