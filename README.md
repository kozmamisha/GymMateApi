# GymMate API

A RESTful **ASP.NET Core 9 Web API** for managing **users, courses, trainings, comments, and subscriptions**.  
Built with **Entity Framework Core**, **JWT Authentication**, and **Clean Architecture** principles. 

---

## 🚀 Features
- User registration & login with JWT authentication
- Role-based authorization (Admin, User)
- Course & training management
- User subscriptions to courses
- Commenting system
- Swagger/OpenAPI documentation
- EF Core with code-first migrations
- Docker support for containerized deployment

---

## 🛠️ Tech Stack
- ASP.NET Core 9
- Entity Framework Core
- PostgreSQL
- Docker
- JWT Bearer Authentication
- Bcrypt
- Scalar
- Clean Architecture

## Getting Started

### 📋 Prerequisites
- .NET 9 SDK
- PostgreSQL

---

### ⚙️ Installation
```bash
git clone https://github.com/kozmamisha/GymMateApi.git
cd GymMateApi
dotnet build
```

---

### Run Database Migrations
```bash
dotnet ef database update
```

---

### Run the API
```bash
dotnet run --project GymMateApi
```

API will be available at:
👉 http://localhost:5000 (or configured port)

---

## 🔐 Local Development Secrets

Connection strings and the JWT signing key are **not stored in the repository**.
They live in [.NET User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets),
outside the project folder (`%APPDATA%\Microsoft\UserSecrets\<UserSecretsId>\secrets.json` on Windows,
`~/.microsoft/usersecrets/<UserSecretsId>/secrets.json` on Linux/macOS).

After cloning, every developer sets them once per service:

```bash
# same signing key must be used by ALL services:
# AuthService issues the token, the others only validate it
KEY="<your-random-key-at-least-32-bytes>"

dotnet user-secrets set "ConnectionStrings:AuthDbContext"      "Host=localhost;Port=5432;Database=gymMate_auth;Username=postgres;Password=<pwd>"      --project GymMateApi.AuthService
dotnet user-secrets set "ConnectionStrings:CommentsDbContext"  "Host=localhost;Port=5432;Database=gymMate_comments;Username=postgres;Password=<pwd>"  --project GymMateApi.CommentsService
dotnet user-secrets set "ConnectionStrings:CoursesDbContext"   "Host=localhost;Port=5432;Database=gymMate_courses;Username=postgres;Password=<pwd>"   --project GymMateApi.CoursesService
dotnet user-secrets set "ConnectionStrings:ExercisesDbContext" "Host=localhost;Port=5432;Database=gymMate_exercises;Username=postgres;Password=<pwd>" --project GymMateApi.ExercisesService
dotnet user-secrets set "ConnectionStrings:TrainingsDbContext" "Host=localhost;Port=5432;Database=gymMate_trainings;Username=postgres;Password=<pwd>" --project GymMateApi.TrainingsService

for p in AuthService CommentsService CoursesService ExercisesService TrainingsService; do
  dotnet user-secrets set "JwtOptions:SecretKey" "$KEY" --project "GymMateApi.$p"
done
```

Check what is stored: `dotnet user-secrets list --project GymMateApi.AuthService`

Non-secret settings (`JwtOptions:ExpiresHours`, `Auth:CookieName`, logging) stay in
`appsettings.Development.json`.

In staging/production do **not** use User Secrets — supply the same keys through
environment variables (`ConnectionStrings__AuthDbContext`, `JwtOptions__SecretKey`)
or a secret manager.
