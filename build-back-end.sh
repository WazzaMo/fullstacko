#!/bin/bash

docker build -t localhost:6000/movieapi -f back-end/dotnetDockerfile --network=host back-end/

docker push localhost:6000/movieapi