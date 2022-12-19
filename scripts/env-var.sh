#! /usr/bin/env bash

imageTag="$(git rev-parse --short head)"
export imageTag