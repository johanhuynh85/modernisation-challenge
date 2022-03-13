# Commission Factory Modernisation Challenge

This repository is used to migrate Modernisation Challenge solution 
- From: .NET Framework 4.8, Linq to SQL, and ASP.NET Web Forms.
- To: .NET 6, Entity Framework Core 6, and ASP.NET Core 6, with the Emberjs front end framework.

**Please prepare the db schema as below**
Create a database called `ModernisationChallenge`, then execute the following script to create the required table:

``` sql
USE [ModernisationChallenge];

CREATE TABLE [Tasks] (
	[Id] INT NOT NULL IDENTITY(1, 1),
	[DateCreated] DATETIME NOT NULL,
	[DateModified] DATETIME NULL,
	[DateDeleted] DATETIME NULL,
	[Completed] BIT NOT NULL,
	[Details] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED ([Id] ASC)
);
```

**Then follow below steps to start this solution locally**
- Start the back-end api [back-end guide](./back-end/README.md)
- Start the fron-end website [front-end guide](/front-end/README.md)