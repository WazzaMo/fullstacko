# FullStacko

A demonstration of a React front-end working with a .NET
back-end.

## Assumptions

The .NET code acts like a REST API proxy of sorts.
The API in the .NET back-end should attempt redundant paths
to ensure best effort service delivery.

The front-end should assume the user is impatient and needs
it to try its best to work with the back-end.

# Special Notes

The `appsettings.json` file has a configuration for the provider API
API-TOKEN value but this SHOULD NOT BE COMMITTED to Git.

`back-end/appsettings.json` is locked for change.

## Locking for change

`git update-index --skip-worktree back-end/appsettings.json`

## Method to unlock if needed

`git update-index --no-skip-worktree back-end/appsettings.json`

## Method to determine what was locked

`git ls-files -v . | grep ^S`



