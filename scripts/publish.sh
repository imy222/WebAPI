#! /usr/bin/env bash
set -euo pipefail

image_name="docker.myob.com/future-makers-academy/imay-webapi"
imageTag="$(git rev-parse --short origin/pipeline)"
 
die() { echo "$1"; exit "${2:-1}"; }

[[ -z "$imageTag" ]] && die "Image Tag Is Empty" || echo "Valid Image Tag"

echo "Building application with Build: ${imageTag}"
IMAGE_TAG=${imageTag} docker-compose build --no-cache publish

echo "Pushing image to Cloudsmith wih Build: ${imageTag}"
docker push "$image_name:${imageTag}"