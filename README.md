# Shaghalny — Job Portal Practice Project

A historical ASP.NET MVC job portal exploring how employers publish vacancies and job seekers browse and apply. The checked-in implementation includes job/category management, applications, CV upload/download paths, saved jobs, and account-related pages.

**Status:** legacy educational application targeting **.NET Framework 4.7.2**. This is classic System.Web MVC, not ASP.NET Core. No production deployment or hosted demo is claimed.

## What is in the repository

| Area | Code evidence |
|---|---|
| Job listing and management | `JobsController`, `Job`, and Razor views |
| Applications and CV files | `HomeController.Apply`, `ApplyForJob`, and download actions |
| Employer/applicant views | `GetJobsByPublisher`, `GetJobsByUser`, and related actions |
| Search and saved jobs | `Search`, `SavedJobs`, and `SavedJobs` model |
| Accounts and roles | ASP.NET Identity / OWIN setup, `AccountController`, `RolesController` |

These are implemented code paths, not a claim that every workflow has passed an end-to-end test.

```text
Jop Offers Website.sln
└── Jop Offers Website/
    ├── Controllers/
    ├── Models/
    ├── Views/
    ├── Migrations/
    ├── App_Start/
    ├── Startup.cs
    ├── Web.config
    └── packages.config
```

## Prerequisites

- Windows with Visual Studio and the ASP.NET/web development workload.
- .NET Framework **4.7.2 targeting/developer pack**.
- SQL Server for a disposable local development database.
- NuGet package restore for the classic `packages.config` project.

This project is not run with `dotnet run`; it uses the classic Visual Studio web application toolchain.

## Local setup

1. Open `Jop Offers Website.sln` in Visual Studio.
2. Restore NuGet packages for the solution.
3. Configure `DefaultConnection` in `Jop Offers Website/Web.config` for your own local SQL Server. The committed value references the original developer's MDF path and is not portable. A local example is:

```xml
<add name="DefaultConnection"
     connectionString="Data Source=localhost;Initial Catalog=ShaghalnyDemo;Integrated Security=True"
     providerName="System.Data.SqlClient" />
```

4. Use only a new, disposable database and synthetic users. Review `Startup.cs` and the EF migrations before applying them: the current startup code seeds a demonstration administrator with hard-coded credentials. Do not expose this application to a network as-is.
5. If EF6 tooling is available in Visual Studio's Package Manager Console, select this web project and apply the committed migrations to the configured disposable database:

```powershell
Update-Database
```

6. Set the web project as the startup project, build, and run using IIS Express. The default MVC route opens `Home/Index`; use the URL printed by Visual Studio rather than assuming a deployed address.

Do not commit real credentials or personal CV files. The repository also contains a root `JobOffersDB` artifact; its contents and portability were not validated, so the setup above does not assume it is a usable or safe database backup.

## Scope and limitations

- Legacy framework/dependency stack; modernization is a separate task.
- Demo account seeding, file uploads/downloads, and record-level authorization need a dedicated security review before any deployment.
- Current documentation does not claim verified candidate shortlisting, hiring outcomes, or production traffic.
- No runtime or database test was performed during the portfolio review; project files, controllers, models, routing, and startup configuration were inspected.
- This documentation preserves application code, data artifacts, and existing history.
- No root project license was found; no license or ownership statement was added.

## Why keep this project?

It shows an earlier full-stack business workflow with MVC controllers, relational models, authentication, and file-handling paths. It is useful as a historical learning artifact, alongside newer backend and .NET AI examples, rather than as a production-ready reference implementation.
