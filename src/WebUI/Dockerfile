FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 8000
EXPOSE 4430

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

# Install node.js for npm
RUN apt-get update && apt-get install -y --no-install-recommends apt-utils
RUN curl -sL https://deb.nodesource.com/setup_12.x | bash -
RUN apt-get install -yq nodejs
# Install npm here so the cache can help reduce the need to re-run it
COPY ["src/WebUI/ClientApp/package.json", "WebUI/ClientApp/"]
COPY ["src/WebUI/ClientApp/package-lock.json", "WebUI/ClientApp/"]
RUN cd WebUI/ClientApp && npm ci

COPY ["src/WebUI/WebUI.csproj", "WebUI/"]
COPY ["src/SharedLib/SharedLib.csproj", "SharedLib/"]
COPY ["NuGet.Config", "."]
RUN dotnet restore "./WebUI/WebUI.csproj"
COPY src/. .
WORKDIR "/src/WebUI"
#RUN dotnet build "WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebUI.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebUI.dll"]
