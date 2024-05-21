# Car Market Analysis App

This application provides tools for analyzing the car market, including tracking car brands, models, and their prices over time. It utilizes data scraped from various automotive websites to provide comprehensive insights into the dynamics of car pricing trends.

## Get Started

#### Environment Requirements:

1. Clone the repository:
   ```
   git clone https://github.com/JUKKarol/CarMarketAnalysis.git
   ```
2. Install .NET 8 from:
   ```
   https://dotnet.microsoft.com/en-us/download/dotnet/8.0
   ```
3. Install Microsoft SQL Server from:
   ```
   https://www.microsoft.com/en-us/sql-server/sql-server-downloads
   ```
4. Install Playwright in cmd:
   ```
   dotnet tool install --global PowerShell
   ```
   ```
   dotnet tool update --global PowerShell
   ```
   ```
   cd /your/repo/dir/api
   ```
   ```
   dotnet tool install --global Microsoft.Playwright.CLI
   ```
   ```
   playwright install
   ```

### Run the Project:

1. Navigate into the solution directory:
   ```
   cd /your/repo/dir
   ```
2. Run the application:
   ```
   dotnet run
   ```

### First Run

1. Use `PUT` request to `api/brand` to update all brands from the website.
2. Use `PUT` request to `api/model` to update all models from the website.
3. Use `PUT` request to `api/car/all` to retrieve all cars from the website and store them in the database. (use `https://www.otomoto.pl/osobowe` as url parameter to scrap all cars or for example `https://www.otomoto.pl/osobowe/bmw` if you want to scrap all cars cars for specified brand)
4. Use `GET` request to `api/car/all` to get all cars from the database.

Now all cars are stored in the database.
Explore the REST endpoints documentation at https://localhost:yourport/swagger/index.html.

## Used Technologies

- .NET 8
- Entity Framework 8
- MSSQL
- HTML Agility Pack
- Playwright
