# SFD – Smart Factory Dispatching API

An ASP.NET Core Minimal API simulating factory dispatching, production line management, maintenance tracking, and automated emergency stop logging.

## Tech Stack
* **Language & Framework:** C#, .NET, ASP.NET Core (Minimal API)
* **Data Access:** Entity Framework Core (SQLite), LINQ
* **API Documentation:** Swagger / OpenAPI

## Project Structure
* `Program.cs` – API endpoints, JSON enum serialization, automatic database seeding, and emergency logging.
* `TovarniDispecinkContext.cs` – EF Core `DbContext` managing database sets and SQLite configuration.
* `VyrobniLinka.cs`, `Zamestnanec.cs`, `ZaznamUdrzby.cs` – Domain models representing production lines, employees, and maintenance records.

## How to Run
1. Clone the repository:
   ```bash
   git clone [https://github.com/OPippal/FactoryAnalytics-CSharp.git](https://github.com/OPippal/FactoryAnalytics-CSharp.git)
   cd FactoryAnalytics-CSharp
   Run the application (the SQLite database tovarna.db is created and seeded automatically on first launch):
   dotnet run
   Open Swagger UI in your browser to explore and test the endpoints:
   http://localhost:5000/swagger/index.html
   
Note: When a production line exceeds the critical temperature threshold (110 °C), an emergency stop event is automatically logged to a local havarijni_log.txt file