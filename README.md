# Project Scope Description
ASP.NET Core Learning project in monolithic architecture using:
1. Domain Driven Design
2. Clean Architecture
3. Repository Pattern
4. EntityFrameworkCore
5. In-memory Database implementation
6. PostgreSQL Database implementation with migrations
7. Swagger
8. Docker

# NuGet Packages used:
1. Microsoft.EntityFrameworkCore
2. Microsoft.EntityFrameworkCore.Design
3. Microsoft.EntityFrameworkCore.Tools
4. Npgsql.EntityFrameworkCore.PostgreSQL

# To run migrations at Package Manager Console:
```
Add-Migration <migration_name>
Update-Database
```

# Create table scripts without using migrations:
```
CREATE TABLE "Employees"("EmployeeId" SERIAL PRIMARY KEY, "EmployeeName" VARCHAR(255) NOT NULL, "Role" INTEGER, "Version" INTEGER DEFAULT 0);
CREATE TABLE "Projects"("ProjectId" SERIAL PRIMARY KEY, "ProjectDescription" VARCHAR(255) NOT NULL, "EmployeeId" INTEGER, "Version" INTEGER DEFAULT 0);
```