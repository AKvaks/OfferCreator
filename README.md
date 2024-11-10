# Offer Creator

This Blazor WebAssembly .NET Core 8 hosted app was developed for a technical interview, emphasizing simplicity and maintainability following the KISS principle.
It uses Entity Framework with a Code-First approach, along with the Repository pattern, MediatR, and CQRS.

## Assignment

Develop a web application for creating, editing, and viewing offers, with CRUD operations in a .NET 6+ REST API for BE, .NET 6+ Blazor for FE, and data stored in MS SQL Server.
The offer entry page should display auto-generated offer numbers and dates, and allow item management (add, edit, delete) within a grid or pop-up modal, showing columns for item, unit price, quantity,
and total price. Client-side and server-side validation are required, and a paginated view should display offer summaries with an edit option.

## Before running the application

> [!NOTE]
> Make sure that you have SQL Server 2022 Express installed and that connection string is set correctly

There are seed methods in Program.cs that will populate database with mock data. A call to these methods can be found from line 56 to 58.

## Migrations

To create a new migration, update database or remove migration use one of the following console commands:

### Create migration

> [!NOTE]
> Position yourself in the solution folder

```console
Add-Migration MIGRATION_NAME -Project OfferCreator.Persistance -StartupProject OfferCreator.API
```

### Update database

> [!NOTE]
> Update Db fom the console or just run the application

```console
Update-Database -Project OfferCreator.Persistance -StartupProject OfferCreator.API
```

### Remove last migration

> [!CAUTION]
> This should be done ONLY IF migration was not applied to the Db

```console
Remove-Migration -Project OfferCreator.Persistance -StartupProject OfferCreator.API
```
