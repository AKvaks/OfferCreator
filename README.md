# Offer Creator

This is a Blazor WebAssembly .NET Core 8 hosted app designed as part of a technical interview.

## Before running the application

> [!NOTE]
> Make sure that you have SQL Server 2022 Express installed and that connection string is set correctly

There are seed methods in Program.cs that can be used to populate database with mock data. A call to these methods are commented out from line 49 to 51.

## Migrations

To create a new migration, update database or remove migration use one of the following console commands:

### Creating migration

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

## Removing last migration

> [!CAUTION]
> This should be done ONLY IF migration was not applied to the Db

```console
Remove-Migration -Project OfferCreator.Persistance -StartupProject OfferCreator.API
```
