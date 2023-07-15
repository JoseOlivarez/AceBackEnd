# Unit Testing Guide for AceBackEnd Project

This guide will assist you with the steps to perform unit tests on the AceBackEnd project.

### Prerequisites

Make sure you have installed .NET SDK on your system. If not, download it from [here](https://dotnet.microsoft.com/download).

## Step 1: Navigate to the test project

Open your terminal/command prompt, navigate to the `AceBackEnd.Tests` project directory:

```bash
cd AceBackEnd.Tests
```

## Step 2: Add a Reference to the Main Project

You need to add a reference to the main project `AceBackEnd` that you're going to test. This can be done by running the following command:

```csharp
dotnet add reference ../AceBackEnd/AceBackEnd.csproj
```

## Step 3: Run Unit Tests with Code Coverage

Execute your unit tests and collect code coverage data with the following command:

```bash
dotnet test --collect:"XPlat Code Coverage" /p:CollectCoverage=true /p:TargetProject=./AceBackEnd.Tests/AceBackEnd.Tests.csproj
```

This command will run all the unit tests in the `AceBackEnd.Tests` project, and it will collect code coverage information during the test run.

## Step 4: Code Coverage Report

After running the tests, a coverage report will be generated. You can find the coverage report file in the following directory:

```
TestResults/{GUID}/coverage.cobertura.xml
```

This `coverage.cobertura.xml` file provides a detailed report of the code coverage, including the percentage of lines covered by the tests and the detailed coverage for each class and method.