#! /usr/bin/env bash
set -euo pipefail
# image name cannot contain capital letters
image_name="joketests:1"
# build image but only run up to test line in docker file. If additional lines added to 
#docker file later, can add as another step in buildkite.yml 
docker build -f Dockerfile.test . -t "${image_name}" --target "test"

docker run "${image_name}"