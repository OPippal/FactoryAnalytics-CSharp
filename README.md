# SFD – Smart Factory Dispatching API

An ASP.NET Core Minimal API simulating factory dispatching, production line management, maintenance tracking, and emergency log processing.

## Tech Stack
* **Language & Framework:** C#, .NET, ASP.NET Core (Minimal API)
* **Data Access:** Entity Framework Core (In-Memory Database), LINQ
* **API Documentation:** Swagger / OpenAPI

## Project Structure
* `Program.cs` – API endpoints, dependency setup, EF Core queries, and log processing.
* `TovarniDispecinkContext.cs` – EF Core `DbContext` managing database sets and in-memory configuration.
* `VyrobniLinka.cs`, `Zamestnanec.cs`, `ZaznamUdrzby.cs` – Domain models representing production lines, employees, and maintenance records.
* `havarijni_log.txt` – Sample emergency log file used for data parsing and analysis.

## How to Run
1. Clone the repository:
   ```bash
   git clone [https://github.com/OPippal/FactoryAnalytics-CSharp.git](https://github.com/OPippal/FactoryAnalytics-CSharp.git)
   cd FactoryAnalytics-CSharp
   Run the application:
   dotnet run
   Open Swagger UI in your browser to explore and test the endpoints:
   http://localhost:5000/swagger/index.html