FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ENV APP_VERSION=1.0.0

WORKDIR /src

COPY ["to do api.sln", "./"]

COPY ["to do api/to do api.csproj", "to do api/"]

RUN dotnet restore "to do api.sln"

COPY . .

RUN dotnet publish "to do api.sln" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

RUN groupadd --gid 8301 OSAGroup && useradd --uid 7201 --gid 8301 --shell /bin/bash --create-home OSAUser

WORKDIR /app

COPY --from=build /app/publish .


ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80

USER OSAUser

ENTRYPOINT ["dotnet", "to do api.dll"]

LABEL maintainer="Rodion Rybko"