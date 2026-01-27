# Objective Summary

This project provides the environment to setup the code for using EntityFramework to access an existing database.

The most convenience approach is to use tool to generate all the entities and the DBContext to match the tables and views of the exiting database.

Within Visual Studio, install the nuget package **Microsoft.EntityFrameworkCore.Tools** which provides various commands to generate scaffolding (reverse engineering) code or migration code.

Next, install the nuget package of the database provider of your choice like **Microsoft.EntityFrameworkCore.SqlServer**

Next run this tool command (Scaffold-DbContext) in the Package Manager Console (PMC) within Visual Studio to generate the code for all the entities and the DBContext.

```poweshell
Scaffold-DbContext 
  -Connection "Name=ConnectionStrings:Default" 
  -Provider Microsoft.EntityFrameworkCore.SqlServer 
  -OutputDir "..\AdvWorkEntity\Entity" 
  -Namespace AdvWorkEntity.Entity 
  -ContextDir "..\AdvWorkEntity" 
  -Context AdvWorkDbContext 
  -ContextNamespace AdvWorkEntity 
  -Force 
  -Project AdvWorkEntity 
  -StartupProject AdvWorkConsoleApp
```

In this project, the code is generated for the sample database **AdventureWorks2019** mounted on a sql server local db instance.
