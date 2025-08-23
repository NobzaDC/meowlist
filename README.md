# MeowList

Purrfect lists for imperfect humans.

## Overview

MeowList is a simple .NET 8 Web API for managing todo lists, tags, and users. It uses Entity Framework Core for data access and supports migrations for database versioning.

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

For more details, see the source code in the `MeowCore/Models` folder and the migration files in `MeowCore/Migrations