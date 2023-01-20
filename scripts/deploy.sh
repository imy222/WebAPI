#!/usr/bin/env bash
set -euo pipefail

imageTag="$(git rev-parse --short origin/pipeline)"

echo "Deploy to Jupiter"
ktmpl ./Deployment/template.yaml -f ./Deployment/default.yaml -p imageTag "${imageTag}"  | kubectl apply -f - --validate=false

