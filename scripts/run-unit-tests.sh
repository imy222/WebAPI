#! /usr/bin/env bash
set -euo pipefail
imageTag="latest"

echo "Build and run API unit test with Build#: ${imageTag}"
IMAGE_TAG=${imageTag} docker-compose up --force-recreate unit-tests
IMAGE_TAG=${imageTag} docker-compose down
