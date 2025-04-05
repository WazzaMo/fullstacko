#!/bin/bash

# Create local registry environment

# Create registry data directory if it doesn't exist
test ! -d registry_data && mkdir -p registry_data

# Start registry container
docker-compose -f containers/registry-compose.yaml up -d

