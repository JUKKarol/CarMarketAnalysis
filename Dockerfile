# Set the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory
WORKDIR /app/api/CarMarketAnalysis

# Copy the .csproj and restore dependencies
COPY api/CarMarketAnalysis/*.csproj ./
RUN dotnet restore

# Copy the entire project and build
COPY api/CarMarketAnalysis/ ./
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app/api/CarMarketAnalysis
COPY --from=build-env /app/api/CarMarketAnalysis/out .

# Copy the appsettings.json file
COPY api/CarMarketAnalysis/appsettings.json .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "CarMarketAnalysis.dll"]