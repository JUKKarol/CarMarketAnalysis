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

#Install powershell to playwright scripts
RUN apt-get update -yq \
    && apt-get install wget -yq \
    && wget -q https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && apt-get update -yq \
    && apt-get install powershell -yq
    
#Install playwright dependencies
RUN pwsh ./playwright.ps1 install
RUN pwsh ./playwright.ps1 install chromium
RUN pwsh ./playwright.ps1 install-deps chromium

# Copy the appsettings.json file
# COPY api/appsettings.json .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "CarMarketAnalysis.dll"]