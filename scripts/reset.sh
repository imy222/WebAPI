#!/usr/bin/env bash   
image_suffix=${BUILDKITE_BUILD_NUMBER}

docker-compose rm -fsv
docker rmi docker.myob.com/future-makers-academy/imay-webapi:$image_suffix

