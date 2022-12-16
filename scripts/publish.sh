#! /usr/bin/env bash
set -euo pipefail

image_name="docker.myob.com/future-makers-academy/imay-webapi"
IMAGE_TAG="${1}"

echo "Building application with Build: ${IMAGE_TAG}"
#docker-compose up --force-recreate publish 
docker-compose build --no-cache publish

echo "Pushing image to Cloudsmith with Build: ${IMAGE_TAG}"
docker tag "imay-webapi:latest" "$image_name:${IMAGE_TAG}"
docker push "$image_name:${IMAGE_TAG}"