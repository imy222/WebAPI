#!/usr/bin/env bash
imageTag="latest"

echo "Build and lint dotnet solution"
IMAGE_TAG=${imageTag} docker-compose up lint
IMAGE_TAG=${imageTag} docker-compose down