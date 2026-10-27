FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/Sprint1/Aspirra.Sprint1.Api/Aspirra.Sprint1.Api.csproj src/Sprint1/Aspirra.Sprint1.Api/
RUN dotnet restore src/Sprint1/Aspirra.Sprint1.Api/Aspirra.Sprint1.Api.csproj

COPY src/Sprint1/Aspirra.Sprint1.Api/ src/Sprint1/Aspirra.Sprint1.Api/
WORKDIR /src/src/Sprint1/Aspirra.Sprint1.Api
RUN dotnet publish Aspirra.Sprint1.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Aspirra.Sprint1.Api.dll"]
