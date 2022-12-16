#! /usr/bin/env bash
set -euo pipefail

image_name="docker.myob.com/future-makers-academy/imay-webapi"
imageTag="${1}"

echo "Building application with Build:" ${imageTag}
#docker-compose up --force-recreate publish 
docker-compose build --no-cache publish

echo "Pushing image to Cloudsmith with Build:" ${imageTag}
docker tag "imay-webapi:latest" "$image_name:$imageTag"
docker push $image_name:$imageTag