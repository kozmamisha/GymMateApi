# scripts

PowerShell helpers so EF Core commands do not have to be typed per service.
Run them from the repository root.

| Script | What it does |
|---|---|
| `update-database.ps1` | applies pending migrations (creates the database if missing) |
| `add-migration.ps1` | adds a migration to **one** service |
| `list-migrations.ps1` | lists migrations and shows which are already applied |
| `reset-databases.ps1` | **destructive** - drops and recreates databases (dev only) |

`-Service` accepts: `Auth`, `Comments`, `Courses`, `Exercises`, `Trainings`.
Omit it on `update-database` / `list-migrations` to hit all five.

```powershell
# first run: start Postgres, then create all five databases
docker compose up -d
./scripts/update-database.ps1

# schema change in one service
./scripts/add-migration.ps1 -Service Courses -Name AddCourseDescription
./scripts/update-database.ps1 -Service Courses

# start over locally
./scripts/reset-databases.ps1 -Force
```

Adding a new service: register it in the `$Services` map in `_common.ps1`,
nothing else changes.

Prerequisites: Docker running (`docker compose up -d`), `.env` present
(copy from `.env.example`), and connection strings in User Secrets -
see *Local Development Secrets* in the root README.
