#! /usr/bin/env bash
set -euo pipefail

image_name="docker.myob.com/future-makers-academy/imay-webapi"
image_suffix=${BUILDKITE_BUILD_NUMBER}

echo "Building application"
#docker-compose up --force-recreate publish 
IMAGE_TAG=${image_suffix} docker-compose build --no-cache publish

echo "Pushing image to Cloudsmith"
docker push $image_name:$image_suffix