#!/usr/bin/env bash
set -euo pipefail

echo "Deploy to Jupiter"
ktmpl ./Deployment/template.yaml -f ./Deployment/default.yaml -p imageTag "$BUILDKITE_BUILD_NUMBER"  | kubectl apply -f - --validate=false

