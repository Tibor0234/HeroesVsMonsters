# HeroesVsMonsters

Console application for managing Heroes, Monsters and recording Battles. Implemented as a semester-long university assignment to demonstrate domain modelling, dependency injection, simple persistence and console-based UX.

## Overview
- Domain models: `Hero`, `Monster`, `Battle`.
- CRUD-style console flows for creating and modifying entities.
- Persistence via an EF-style abstraction `IHeroesVsMonstersDbContext` (concrete implementation under `Persistence.MsSql`).
- Input validation and parsing handled by `IAppendOrModifyEntityService` and `IValidation`.
- Small, self-contained codebase designed for clarity and easy extension.

## Features
- Add and modify Heroes and Monsters from the console.
- Abilities parsing for `Hero` (comma-separated input).
- Persisted entities using a DbContext abstraction (`DbSet<T>` semantics).
- Simple listing helpers to enumerate stored entities before editing.

## Tech stack
- .NET (console application)
- C#
- EF-style DbContext abstraction (concrete SQL persistence available)
- NuGet dependencies (e.g., Humanizer)

## Quickstart (Visual Studio 2022)
1. __Open a project or solution__ in Visual Studio 2022.
2. Restore NuGet packages: right-click solution → __Restore NuGet Packages__.
3. Set the startup project: right-click the console project → __Set as Startup Project__.
4. Configure the database connection for the concrete `IHeroesVsMonstersDbContext`.
5. Run: __Debug > Start Debugging__ or press Ctrl+F5.

Quickstart (dotnet CLI)
- Restore: `dotnet restore`
- Build: `dotnet build`
- Run: `dotnet run --project C2K2DP_HSZF_2024251`

If you prefer to run without a SQL backend, implement an in-memory/mock `IHeroesVsMonstersDbContext` and register it in place of the MsSql implementation.

## Typical usage (console)
- Choose entity: press `H` for Hero or `M` for Monster.
- Hero input: `Name`, `Category` (`C`, `B`, `A`, `S`), `Strength` (1–100), `Speed` (1–100), `Abilities` (comma-separated).
- Monster input: `Name`, `Level` (`Vampire`, `Daemon`, `Golem`, `Dragon`), `Strength`, `Speed`.
- Modify flows allow leaving fields blank to keep current values.

## Important files & interfaces
- `AppendOrModifyEntity.cs` — console flows for append/modify operations.
- `Model/` — `Hero`, `Monster`, `Battle`.
- `Persistence/MsSql/` — concrete DbContext implementation.
- `Application/` — `IAppendOrModifyEntityService`, `IValidation`, `IListEntities`, `IHeroAbilities`.

## Testing & extensions
- Interfaces make unit testing straightforward (mock `IHeroesVsMonstersDbContext` and services).
- Suggested improvements: unit tests for validation, making abilities have an affect.

Notes: developed as a university semester project — scope and design reflect that context.
