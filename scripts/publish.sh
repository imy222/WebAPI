#! /usr/bin/env bash
set -euo pipefail

image_name="docker.myob.com/future-makers-academy/imay-webapi"
imageTag="$(git rev-parse --short head)"

echo "Building application with Build: ${imageTag}"
IMAGE_TAG=${imageTag} docker-compose build --no-cache publish

echo "Pushing image to Cloudsmith wih Build: ${imageTag}"
docker push "$image_name:${imageTag}"