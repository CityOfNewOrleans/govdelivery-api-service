# .NET Core GovDelivery API Service library and sample app.

Build requirements:

* .NET Core CLI
* VS Code or VS 2017+
* Any Entity Framework Core supported database (`appSettings.example.json` is configured for SQL Server LocalDb)

Copy the `appSettings.example.json` (found in the `GovDelivery.ConsoleApp`) file and rename the copy to `appSettings.json`

To Create the database:

```
cd .\GovDelivery.ConsoleApp
dotnet ef database update
```

Before building console app:

```
dotnet restore

dotnet restore -r win7-x64
```