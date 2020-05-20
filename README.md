# docker-dotnet-vscode

Samples for developing .NET Core applications with Docker and Visual Studio Code.

## Example Docker Build Commands

Run from the src folder:

    docker build --rm --pull -f src\WebApi\Dockerfile -t webapi .

    docker build --rm --pull -f src\WebUI\Dockerfile -t webui .

    --build-arg PAT=

## Debugging

Create the container network in Docker:

    docker network create demo-net

Example Docker run coommands:

    docker run -dt --name "webapi-dev" --network "demo-net" -e "NO_PROXY=.demo-net, .internal" -e "no_proxy=.demo-net, .internal" -e "ASPNETCORE_ENVIRONMENT=DevContainer" -e "ASPNETCORE_URLS=http://+:80" -p "5000:80" -p "44386:443" "webapi:latest"

    docker run -dt --name "webui-dev" --network "demo-net" -e "NO_PROXY=.demo-net" -e "no_proxy=.demo-net" -e "ASPNETCORE_ENVIRONMENT=DevContainer" -e "ASPNETCORE_URLS=http://+:80" -p "5001:80" -p "44387:443" "webui:dev"
