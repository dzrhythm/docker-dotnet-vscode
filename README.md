# docker-dotnet-vscode

Samples for developing .NET Core applications with Docker and Visual Studio Code.

## Example Docker Build Commands

Run from the src folder:

    docker build --rm --pull -f src\WebApi\Dockerfile -t webapi .

    docker build --rm --pull -f src\WebUI\Dockerfile -t webui .

## Debugging

Create the container network in Docker:

    docker network create demo-net
