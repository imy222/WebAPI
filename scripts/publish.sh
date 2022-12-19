#! /usr/bin/env bash
set -euo pipefail
source ./env-var.sh

image_name="docker.myob.com/future-makers-academy/imay-webapi"
#imageTag="$(git rev-parse --short head)"
#source ./scripts/run-unit-tests.sh
echo $imageTag

echo "Building application with Build: ${imageTag}"
IMAGE_TAG=${imageTag} docker-compose build --no-cache publish

echo "Pushing image to Cloudsmith wih Build: ${imageTag}"
docker push "$image_name:${imageTag}"