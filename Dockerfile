# Set the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set the working directory
WORKDIR /app/api

# Copy the .csproj and restore dependencies
COPY api/*.csproj ./
RUN dotnet restore

# Copy the entire project and build
COPY api/ ./
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app/api
COPY --from=build-env /app/api/out .

# Copy the appsettings.json file
COPY api/appsettings.json .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "CarMarketAnalysis.dll"]