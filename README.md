# MeowList

Purrfect lists for imperfect humans.

## Overview

MeowList is a simple .NET 8 Web API for managing todo lists, tags, and users. It uses Entity Framework Core for data access and supports migrations for database versioning.

## Project Structure

### Data Folder (`MeowCore/Data`)

Contains repository classes and interfaces for data access.  
Repositories handle CRUD operations and queries for each model (Users, Lists, Todos, Tags).

- **Repositories:**  
  - `UsersRepository.cs`
  - `ListsRepository.cs`
  - `TodosRepository.cs`
  - `TagsRepository.cs`
- **Interfaces:**  
  - `Interfaces/IUsersRepository.cs`
  - `Interfaces/IListsRepository.cs`
  - `Interfaces/ITodosRepository.cs`
  - `Interfaces/ITagsRepository.cs`

**Usage:**  
Repositories are injected into services and provide direct access to the database using Entity Framework Core.

### Service Folder (`MeowCore/Service`)

Contains service classes and interfaces for business logic.  
Services validate input, apply business rules, and call repository methods.

- **Services:**  
  - `UsersService.cs`
  - `ListsService.cs`
  - `TodosService.cs`
  - `TagsService.cs`
- **Interfaces:**  
  - `Interfaces/IUsersService.cs`
  - `Interfaces/IListsService.cs`
  - `Interfaces/ITodosService.cs`
  - `Interfaces/ITagsService.cs`

**Usage:**  
Services are injected into controllers and act as the main layer for application logic, ensuring data integrity and validation before accessing repositories.

---

## Unit Tests (`MeowCore.Test`)

The `MeowCore.Test` project contains **unit tests** for controllers, services, repositories, and helpers using **xUnit** and **Moq**.

### Structure

- **Controllers/**  
  Tests for each API controller, validating endpoint responses and error handling.
- **Service/**  
  Tests for business logic in service classes, using mocked repositories.
- **Data/**  
  Tests for repository methods, using an in-memory database for isolation.
- **Helpers/**  
  Tests for utility classes like `ApiResponse` and `JwtService`.

### How Tests Work

- **Controllers:**  
  Mock dependencies (services, loggers, etc.) and verify correct HTTP responses for each endpoint.
- **Services:**  
  Mock repositories and validate business logic, including edge cases and error scenarios.
- **Repositories:**  
  Use `Microsoft.EntityFrameworkCore.InMemory` to simulate a database, seed mock data, and verify CRUD operations.
- **Helpers:**  
  Test static methods and token generation logic.

### Running Tests

To run all tests:

```powershell
dotnet test MeowCore.Test
```

### Example Test

```csharp
[Fact]
public async Task GetUsersAsync_ReturnsOk()
{
    var result = await _controller.GetUsersAsync();
    var okResult = Assert.IsType<OkObjectResult>(result);
    var response = Assert.IsType<ApiResponse<List<Users>>>(okResult.Value);
    Assert.True(response.IsSuccess);
}
```

### Test Coverage

- **Controllers:** All endpoints for Users, Lists, Tags, Todos, Health.
- **Services:** All main business logic methods.
- **Repositories:** All CRUD and query methods.
- **Helpers:** ApiResponse and JwtService.

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (or compatible database)

### Setup

1. **Clone the repository:**
   ```powershell
   git clone https://github.com/NobzaDC/meowlist.git
   cd meowlist/MeowCore
   ```

2. **Restore dependencies:**
   ```powershell
   dotnet restore
   ```

3. **Update connection string:**
   Edit `appsettings.json` to set your database connection string.

### Database Migration

MeowList uses Entity Framework Core migrations to manage the database schema.

#### Apply Initial Migration

1. **Create the database (if not exists):**
   Ensure your connection string points to a valid SQL Server instance.

2. **Apply the initial migration:**
   ```powershell
   dotnet ef database update
   ```

   This will apply the migration found in `Migrations/20250823175152_InitialCreate.cs` and create all necessary tables.

#### Migration Details

- **Migration Name:** InitialCreate
- **Tables Created:**
  - Users
  - Lists
  - Todos
  - Tags
  - TodosTags (many-to-many relation)
- **Model files:** See `MeowCore/Models/`

#### Initial Users

After running the migration, the database will contain the following initial users:

> Passwords in plain text for reference:
> - Admin: `Admin123!`
> - Others: `abc123!`

| Username   | Email                   | Display Name | Role   | Password   |
|------------|-------------------------|--------------|--------|------------|
| admin      | catmin@meowlist.pur     | Catmin       | Admin  | Admin123!  |
| Luna       | fishlover@meowlist.pur  | FishLover    | User   | abc123!    |
| Milo       | purrington@meowlist.pur | Purrington   | User   | abc123!    |
| Nala       | softpaw@meowlist.pur    | Softpaw      | User   | abc123!    |

*(If you want to change or add users, edit the seed logic in `MeowDbContext.cs`.)*

### Running the API

Start the API with:

```powershell
dotnet run
```

The API will be available at `https://localhost:5001` (or as configured).

---

## LogHelper Documentation

The `LogHelper` class (located in `Helpers/LogHelper.cs`) provides a generic logging utility for API requests and responses.

### Features

- Tracks request and response data, status codes, endpoint info, and exceptions.
- Supports audit logging in JSON format using the built-in logger.

### Usage

**Create an initial log:**
```csharp
var log = LogHelper<RequestType, ResponseType>.GetInitialLog(
    controllerName,
    endpointName,
    endpointParams,
    requestObject
);
```

**Update log with response:**
```csharp
log = LogHelper<RequestType, ResponseType>.GetUpdatedLog(log, responseObject, statusCode);
```

**Log an error:**
```csharp
log = LogHelper<RequestType, ResponseType>.GetErrorLog(
    log,
    statusCode,
    exception.Message,
    exception.InnerException?.Message ?? "",
    exception.StackTrace ?? ""
);
```

**Audit log (writes to ILogger):**
```csharp
LogHelper<RequestType, ResponseType>.Audit(logger, log);
```

### Properties

- `id`: Unique identifier for the log entry.
- `controller`: Controller name.
- `endpoint`: Endpoint name.
- `endPointParams`: List of endpoint parameters.
- `statusCode`: HTTP status code.
- `request`: Request object.
- `response`: Response object.
- `exceptionMessage`, `exceptionInnerException`, `exceptionStackTrace`: Exception details.

---

## Logging (Serilog)

MeowList uses **Serilog** for structured logging.

### How does the logger work?

- The logger is configured in `Program.cs` using Serilog.
- Logs are written to:
  - A local file (default: `C:\whiskerWatch\Logs\Index.txt`)
  - The debug console

**Example configuration in `Program.cs`:**
```csharp
builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.Debug(outputTemplate: DateTime.Now.ToString())
        .WriteTo.File(builder.Configuration.GetSection("AppSettings")["LogPathFile"] ?? defaultLogPathFile, rollingInterval: RollingInterval.Day);
});
```

### Custom configuration

You can change the log file path in `appsettings.json`:

```json
"AppSettings": {
  "LogPathFile": "C:\\whiskerWatch\\Logs\\Index.txt"
}
```

---

**Summary:**  
- Logs are sent to a file and to the debug console.
- You can review logs in the configured file or in the debug output.

---

For more details, see the source code in the `MeowCore/Models` folder, the migration files in `MeowCore/Migrations`, and the `Helpers/LogHelper.cs` file.