# FullStacko

A demonstration of a React front-end crossing swords with a .NET
back-end.

# How to run the application

## Notes

I had intended for the front-end and back-end to both run in Docker
using compose (maybe swarm would have been better for this...).

The problem being that the back-end container appears to be unable
to connect to the downstream provider APIs. The Back-end is reachable
from the front-end but cannot return movies. Putting the network_mode to "host"
should have avoided this but something else seems to be going on.

Due to time limitations, I put an end to this for now.

As a result, the back-end and front end need to be started directly

`:-(`

## Starting directly.

1. Modify back-end/appsetting.json `API-TOKEN` value with the real one you want to use.

2. Start the back-end in one terminal

```
cd back-end
dotnet run
```

3. In another terminal, start the front-end

```
npm install
npm start
```

4. Take a look at the movie-list.


# The Docker Dream

A noteworthy point on my thoughts here...

In the `containers` directory there is a docker compose file for a registry.
This allowed me to build, tag and run container images without pushing them
to Docker Hub. I did not want to publish the containers in Docker Hub because
it was "internal" code.

In a proper cloud environment, I would rely on a controlled Azure container registry
or AWS ECR for this.


## Assumptions

The .NET code acts like a REST API proxy of sorts.
The API in the .NET back-end should attempt redundant paths
to ensure best effort service delivery.

The front-end should assume the user is impatient and needs
it to try its best to work with the back-end.

# Special Notes

The `appsettings.json` file has a configuration for the provider API
API-TOKEN value but this SHOULD NOT BE COMMITTED to Git.

Locking the file this way allows me to configure the app and work on it without fear
of committing a credential secret to the Git Repo.

`back-end/appsettings.json` is locked for change.

## Locking for change

`git update-index --skip-worktree back-end/appsettings.json`

## Method to unlock if needed

`git update-index --no-skip-worktree back-end/appsettings.json`

## Method to determine what was locked

`git ls-files -v . | grep ^S`



