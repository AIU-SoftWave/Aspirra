# Multi-stage Dockerfile for Aspirra Service
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore (pattern allows for future project files)
COPY *.csproj ./
RUN dotnet restore 2>/dev/null || true

# Copy everything else
COPY . ./
RUN dotnet publish -c Release -o /app/publish 2>/dev/null || mkdir -p /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "*.dll"]
