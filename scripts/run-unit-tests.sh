#! /usr/bin/env bash
set -euo pipefail

imageTag="${1}"
echo "Build and run API unit test with Build#:" ${imageTag}
docker-compose up unit-tests
docker-compose down
