# modernisation-challenge

This README outlines the details of collaborating on this .Net application.
A short introduction of this app could easily go here.

## Prerequisites

You will need the following things properly installed on your computer.

* [.Net 6.0](https://dotnet.microsoft.com/en-us/download)
* [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) or [Visual Studio Code](https://code.visualstudio.com/download)

## Unit Tests & Integration Tests

**By Visual Studio 2022**
* Using TestExplorer tool to execute test projects

**By Visual Studio Code**
* `dotnet test .\tests\ModernisationChallenge.UnitTests\ModernisationChallenge.UnitTests.csproj`
* `dotnet test .\tests\ModernisationChallenge.IntegrationTests\ModernisationChallenge.IntegrationTests.csproj`

## Running / Development
**By Visual Studio 2022**
* Open `ModernisationChallenge.sln`
* Set "ModernisationChallenge.API` project as Startup project
* Running solution with `IIS Express`  

**By Visual Studio Code**
* `dotnet run --project .\src\ModernisationChallenge.API\ModernisationChallenge.API.csproj`

## Note
* This is Web Api application. We can use token-based authentication to prevent CSRF attacks.