# DotNetTransform01 — Legacy .NET Framework 4.8 Demo

A sample **ASP.NET MVC 5 / .NET Framework 4.8** application used to demonstrate **AWS Transform** (formerly known as the AWS .NET Modernization tool).

## What's in here

| Area | Technology |
|------|-----------|
| Web framework | ASP.NET MVC 5 (`System.Web.Mvc 5.2.7`) |
| Target framework | .NET Framework 4.8 |
| ORM | Entity Framework 6.4.4 (synchronous API) |
| Serialization | Newtonsoft.Json 13.0.1 |
| Package management | NuGet `packages.config` |
| Configuration | `Web.config` / `Web.Release.config` transforms |
| Views | Razor (`.cshtml`) with `Html.Helper` patterns |

## Application

**Product Catalog** — a CRUD web app for managing products and categories.

- `HomeController` — landing page
- `ProductsController` — list, search, create, edit, delete (soft-delete)
- `ProductRepository` — synchronous repository over EF6 (`DbContext`)
- `ApplicationDbContext` — EF6 context targeting SQL Server LocalDB

## Legacy patterns that Transform will modernize

- Synchronous controller actions (no `async`/`await`)
- `packages.config` instead of `<PackageReference>`
- `System.Web`-based hosting (vs. `Microsoft.AspNetCore`)
- `Web.config` XML configuration (vs. `appsettings.json`)
- `EventLog.WriteEntry` for logging (vs. `ILogger`)
- Direct `new ProductRepository()` instantiation (no DI container)
- `HttpApplication` / `Global.asax` app lifecycle

## Getting started

Open `ProductCatalog.sln` in Visual Studio 2019/2022 with the .NET Framework 4.8 developer pack installed. Restore NuGet packages, then run.
.Net Transformation demo application

