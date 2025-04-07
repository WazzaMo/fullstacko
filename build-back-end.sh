#!/bin/bash

# docker buildx build --add-host=host.docker.internal:host-gateway -t localhost:6000/movieapi -f back-end/dotnetDockerfile --network=host back-end/
docker buildx build --allow=network.host -t localhost:6000/movieapi -f back-end/dotnetDockerfile --network=host back-end/

docker push localhost:6000/movieapi