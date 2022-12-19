#! /usr/bin/env bash
set -euo pipefail

imageTag="${1}"

echo "Build and run API unit test with Build#: ${imageTag}"
IMAGE_TAG=${imageTag} docker-compose up --force-recreate --abort-on-container-exit unit-tests
