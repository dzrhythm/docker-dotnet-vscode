# docker-dotnet-vscode

Samples for developing and debugging .NET Core applications with Docker and Visual Studio Code.

## Prerequisites

- [Docker](https://www.docker.com/products/docker-desktop)
- [Visual Studio Code](https://code.visualstudio.com/)
- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [Node.js](https://nodejs.org/)
- [Aurelia CLI](https://aurelia.io/docs/cli/basics)

## Example Docker Build Commands

Run from the src folder:

    docker build --rm --pull -f src\WebApi\Dockerfile -t webapi .

    docker build --rm --pull -f src\WebUI\Dockerfile -t webui .

For the Azure Artifacts demo, create and pass in the personal access token build argument, for example:

    docker build --rm --pull -f src\WebApi\Dockerfile -t webapi --build-arg PAT=[YOUR PAT HERE] .

## Debugging

Create the container network in Docker:

    docker network create demo-net

Be sure to choose the "Docker .NET Core Launch" VS Code debugging target in both WebApi and WebUI.

## Running Containers Locally Without Debugging

Example Docker run commands:

    docker run -dt --name "webapi-dev" --network "demo-net" -e "NO_PROXY=.demo-net, .internal" -e "no_proxy=.demo-net, .internal" -e "ASPNETCORE_ENVIRONMENT=DevContainer" -e "ASPNETCORE_URLS=http://+:80" -p "5000:80" -p "44386:443" "webapi:latest"

    docker run -dt --name "webui-dev" --network "demo-net" -e "NO_PROXY=.demo-net" -e "no_proxy=.demo-net" -e "ASPNETCORE_ENVIRONMENT=DevContainer" -e "ASPNETCORE_URLS=http://+:80" -p "5001:80" -p "44387:443" "webui:dev"
