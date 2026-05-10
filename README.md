# Clean Architecture + DDD + .NET

A simple and clean starter template demonstrating the fundamentals of:

- Clean Architecture
- Domain-Driven Design (DDD)
- CQRS
- ASP.NET Core

Built with modern .NET practices and a layered architecture approach.

---

## 🚀 Technologies

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- MediatR
- FluentValidation
- xUnit

---

## 📂 Project Structure

```text
src/
 ├── Application
 ├── Domain
 ├── Infrastructure
 └── Presentation
```

---

## 📐 Architecture Overview

### Domain
Contains core business rules and domain models.

### Application
Contains use cases, CQRS handlers and abstractions.

### Infrastructure
Contains database access and external implementations.

### Presentation
Contains API endpoints and HTTP related concerns.

---

## ⚡ CQRS Structure

```text
Application/
 └── Features/
      └── Products/
           ├── Commands/
           └── Queries/
```

---

## 🧪 Running Tests

```bash
dotnet test
```

---

## ▶️ Running the Application

```bash
dotnet restore
dotnet build
dotnet run --project src/Presentation
```

---

## 🐳 Docker

```bash
docker compose up --build
```

---

## 🎯 Purpose of This Repository

This repository was created to practice and demonstrate:

- Layered architecture
- Separation of concerns
- DDD building blocks
- CQRS fundamentals
- Maintainable backend structure

---

## 📄 License

MIT
