# Hivify Backend

Hivify is a modular .NET 10 backend for association and property management. It is organized as a single solution with a controller-based HTTP API, separate Control Plane and App Plane bounded contexts, shared application ports and infrastructure, Entity Framework Core persistence, and an optional local AI integration.

## Contents

- [Architecture](#architecture)
- [Repository layout](#repository-layout)
- [Prerequisites](#prerequisites)
- [Local setup](#local-setup)
- [Configuration](#configuration)
- [Running the API](#running-the-api)
- [HTTP API](#http-api)
- [Database and migrations](#database-and-migrations)
- [AI integration](#ai-integration)
- [Tests](#tests)
- [Conventions](#conventions)
- [Troubleshooting](#troubleshooting)
- [Contributing](#contributing)

## Architecture

The solution follows a layered architecture within each bounded context:

`Domain` → `Application` → `Infrastructure` → `Hivify.Api`

- **Control Plane** manages users, associations, invitations, and onboarding.
- **App Plane** contains tenant-facing association features such as houses, departments, feeds, complaints, and documents.
- **BuildingBlocks** provides reusable application ports and infrastructure for CQRS messaging, current-user and tenant access, email, and file storage.
- **SharedKernel** contains common entities, value objects, domain events, aggregate abstractions, and domain exceptions.
- **Hivify.AI** registers the chat client and feed-generation services.
- **Hivify.Api** composes the services, configures authentication and tenant resolution, exposes controllers, and owns the HTTP pipeline.

Authentication uses ASP.NET Core Identity cookies. Tenant resolution uses Finbuckle.MultiTenant and the host strategy `__tenant__.localhost`; the tenant identifier is resolved from the association store.

## Repository layout

```text
Hivify.slnx
Hivify.Api/                         HTTP API, controllers, startup, configuration
Hivify.AI/                          Ollama chat client and feed-generation services
BuildingBlocks/                     Shared application ports and infrastructure
SharedKernel/                       Shared domain abstractions and value objects
src/
  ControlPlane/
    AssociationsMgmt/               Association domain, application, infrastructure
    Identity/                       Identity domain, application, infrastructure
    OnBoarding/                     Onboarding domain, application, infrastructure
  AppPlane/
    Complaints/                     Domain, application, infrastructure, tests
    Departments/                    Domain, application, infrastructure, tests
    DocumentsMgmt/                  Document application and infrastructure projects
    Feeds/                          Domain, application, infrastructure, tests
    Houses/                         Domain, application, infrastructure
Taskfile.yml                        EF migration tasks
HifiyBackend-Diagrams.drawio        Architecture diagrams
```

All projects target `net10.0`. The repository currently contains the backend solution; `node_modules` and `package-lock.json` are not an active frontend project definition.

## Prerequisites

- .NET 10 SDK
- SQL Server or SQL Server LocalDB
- PowerShell (recommended on Windows)
- Visual Studio 2026 with the .NET workload, or VS Code with C# tooling
- Ollama with the `phi4-mini` model if using the AI services
- Task (optional) for the commands in `Taskfile.yml`

## Local setup

Run commands from the repository root:

```powershell
dotnet restore Hivify.slnx
dotnet build Hivify.slnx -c Debug
```

The default connection string targets SQL Server LocalDB and creates a database named `Hivify`. Update `Hivify.Api/appsettings.Development.json` or use configuration overrides for another SQL Server instance.

Before starting the API, configure the required Resend API key. For local development, the API still registers the Resend client even though it uses the development email sender:

```powershell
$env:Resend__ApiKey = "<resend-api-key>"
```

Cloudinary credentials are required when file storage is used:

```powershell
$env:Cloudinary__CloudName = "<cloud-name>"
$env:Cloudinary__ApiKey = "<api-key>"
$env:Cloudinary__ApiSecret = "<api-secret>"
```

Do not commit credentials to `appsettings*.json`.

## Configuration

The API reads the following settings:

| Setting | Purpose |
| --- | --- |
| `ConnectionStrings:DefaultConnection` | SQL Server connection used by the bounded-context DbContexts |
| `Resend:ApiKey` | Resend API token used by the production email sender and required during startup |
| `Resend:FromEmail` | Sender address for email messages |
| `Cloudinary:CloudName`, `ApiKey`, `ApiSecret` | Cloudinary file storage credentials |
| `Frontend:EmailConfirmationUrl` | URL included in email-confirmation links |

Use the ASP.NET Core environment variable format, for example `ConnectionStrings__DefaultConnection` and `Frontend__EmailConfirmationUrl`.

## Running the API

Start the API project with:

```powershell
dotnet run --project Hivify.Api/Hivify.Api.csproj
```

The development launch profile provides the HTTP URL. HTTPS is enabled by the API pipeline. Swagger UI and OpenAPI are mapped only when `ASPNETCORE_ENVIRONMENT=Development`.

The API seeds Identity data during startup. The database must be reachable and the required configuration must be available before the application can start successfully.

## HTTP API

Controllers are grouped by bounded context under `Hivify.Api/Controllers`:

| Area | Base routes |
| --- | --- |
| Identity | `/api/auth` |
| Onboarding | `/api/onboarding` |
| Platform associations | `/api/platform-admin/associations` |
| Public invitations | `/api/invitations` and `/api/associations/invitations` |
| Tenant administration | `/api/tenant-admin/association` and `/api/tenant-admin/invitations` |
| Houses | `/api/houses` |
| Departments | `/api/departments` |
| Feeds | `/api/feeds` |
| Complaints | `/api/complaints` |
| Diagnostics | `/api/debug` |

Authentication and authorization vary by endpoint. The implementation uses authenticated users, the `Admin` role, the `AssociationAdmin` policy, and tenant-admin authorization where applicable. Use the controller attributes as the authoritative source for access requirements.

## Database and migrations

Each persistence-focused bounded context owns its EF Core `DbContext` and migrations. The repository provides a `Taskfile.yml` with add and update tasks for Identity, Houses, Departments, Associations, Onboarding, Complaints, and Feeds.

Using Task:

```powershell
task migrate-all
task add-house-migration migrationName=AddExampleChange
task migrate-house
```

Using the .NET CLI directly:

```powershell
dotnet ef migrations add <MigrationName> `
  --context HouseDbContext `
  --project src/AppPlane/Houses/Infrastructure/Houses.Infrastructure.csproj `
  --startup-project Hivify.Api/Hivify.Api.csproj

dotnet ef database update `
  --context HouseDbContext `
  --project src/AppPlane/Houses/Infrastructure/Houses.Infrastructure.csproj `
  --startup-project Hivify.Api/Hivify.Api.csproj
```

Replace the context and project for the bounded context being changed. Do not combine migrations from different contexts into one migration history.

## AI integration

`Hivify.AI` registers an `IChatClient`, `IHivifyAgent`, and `IFeedGenerator`. The current implementation connects to a local Ollama server at `http://localhost:11434` and uses the `phi4-mini` model.

```powershell
ollama pull phi4-mini
ollama serve
```

The Ollama endpoint and model are currently defined in `Hivify.AI/AICollectionExtensions.cs` rather than configuration.

## Tests

Run all tests in the solution:

```powershell
dotnet test Hivify.slnx -c Debug
```

Domain test projects currently exist for Complaints, Departments, and Feeds. Run a specific test project when isolating a failure:

```powershell
dotnet test src/AppPlane/Complaints/Tests/Domain.Tests/Domain.Tests.csproj
```

## Conventions

- Keep dependencies flowing from Domain to Application to Infrastructure; API composition belongs in `Hivify.Api`.
- Put repository implementations and EF mappings in the relevant Infrastructure project.
- Keep application contracts, commands, queries, and ports independent of infrastructure details.
- Use shared value objects such as `AssociationID`, `UserID`, `Name`, `Email`, and `PhoneNumber` where the domain requires them.
- Treat tenant context and current-user access as application ports rather than reading HTTP details in domain code.
- Add migrations to the Infrastructure project that owns the affected `DbContext`.

## Troubleshooting

- **Startup fails because `Resend:ApiKey` is missing:** set `Resend__ApiKey` even when running in Development.
- **Database connection errors:** verify SQL Server/LocalDB is running and `DefaultConnection` is correct, then apply the required migrations.
- **AI requests fail:** start Ollama and verify that the `phi4-mini` model is installed.
- **Tenant resolution fails:** use a host such as `<tenant>.localhost`; tenant resolution is configured with the `__tenant__.localhost` host strategy.
- **CS0006 metadata-file errors:** build the referenced project directly, then clean `bin` and `obj` and rebuild the solution.
- **EF extension methods are missing:** verify the relevant Infrastructure project references the required EF Core relational package.

Clean a project before rebuilding:

```powershell
dotnet clean Hivify.slnx
dotnet build Hivify.slnx -c Debug
```

## Contributing

1. Create a feature branch from the repository's active development branch.
2. Keep changes inside the appropriate bounded context and layer.
3. Add or update tests for domain behavior.
4. Add or update the relevant EF migration when persistence changes.
5. Run `dotnet build Hivify.slnx` and `dotnet test Hivify.slnx`.
6. Open a pull request with a summary and reproducible validation steps.
