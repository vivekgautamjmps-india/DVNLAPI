# DVNLAPI

Simple ASP.NET Core Web API (.NET 8, C#) with in-memory CRUD for `Item` entities and Swagger UI.

## Run locally

```bash
cd DVNLAPI
dotnet restore
dotnet run
```

Swagger UI opens automatically at: `https://localhost:7157/swagger` (or the URL shown in console).

## Endpoints

| Method | Route              | Description         |
|--------|---------------------|----------------------|
| GET    | /api/items           | Get all items        |
| GET    | /api/items/{id}       | Get item by id        |
| POST   | /api/items            | Create a new item      |
| PUT    | /api/items/{id}       | Update an existing item |
| DELETE | /api/items/{id}       | Delete an item        |
| GET    | /health               | Health check          |

## Sample request body (POST / PUT)

```json
{
  "name": "Keyboard",
  "description": "Mechanical keyboard",
  "price": 2499.00
}
```

## Push to GitHub (DVNLAPI repo)

```bash
git init
git add .
git commit -m "Initial commit: simple .NET 8 Web API"
git branch -M main
git remote add origin https://github.com/vivekgautamjmps-india/DVNLAPI.git
git push -u origin main
```
