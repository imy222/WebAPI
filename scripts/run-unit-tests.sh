#! /usr/bin/env bash
set -euo pipefail

imageTag="${1}"

echo "Build and run API unit test with Build#: ${imageTag}"
IMAGE_TAG=${imageTag} docker-compose up unit-tests
IMAGE_TAG=${imageTag} docker-compose down
