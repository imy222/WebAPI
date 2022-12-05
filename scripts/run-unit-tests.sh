#! /usr/bin/env bash
set -euo pipefail

docker-compose up unit-tests
docker-compose down
