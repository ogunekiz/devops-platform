FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.sln .
COPY DevOpsPlatform.Api/*.csproj DevOpsPlatform.Api/
COPY DevOpsPlatform.Application/*.csproj DevOpsPlatform.Application/
COPY DevOpsPlatform.Domain/*.csproj DevOpsPlatform.Domain/
COPY DevOpsPlatform.Infrastructure/*.csproj DevOpsPlatform.Infrastructure/

RUN dotnet restore

COPY . .
WORKDIR /src/DevOpsPlatform.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080

HEALTHCHECK --interval=30s --timeout=5s \
 CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "DevOpsPlatform.Api.dll"]
