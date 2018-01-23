$projectLocation = Get-Location;

function Test-BuiltProject {
    param([Parameter(Mandatory)][Scriptblock]$callback)
    
    Write-Host " Building project...`r`n"
    & dotnet build;

    if ($LastExitCode -eq 0) {
        Write-Host "`r`n Project build successful. Running tests now.. `r`n"
        & $callback
    }
    else {
        Write-Host "`r`n Build Failed. Aborting test. `r`n" -ForegroundColor Red
    }
}

function Publish-ConsoleApp {
    & dotnet publish $projectLocation\GovDelivery.ConsoleApp\GovDelivery.ConsoleApp.csproj -c release -r win7-x64;
}

function Invoke-ReleaseBuild {
    & dotnet pack -c "Release" $projectLocation\GovDelivery.Nuget --include-symbols --include-source
}

function Invoke-ConsoleApp {

    #& dotnet run -p .\GovDelivery.ConsoleApp\GovDelivery.ConsoleApp.csproj -- $args;
    & $projectlocation\GovDelivery.ConsoleApp\bin\release\netcoreapp1.1\win7-x64\GovDelivery.ConsoleApp.exe $args;

}

function Test-Library {
    & dotnet test $projectLocation\GovDelivery.Tests\GovDelivery.Tests.csproj;
}

function Push-Nuget {

    if ((Test-Path "$projectLocation\.nugetApiKey") -eq $false) {
        Write-Host "Nuget API key not found. Create a '.nugetApiKey' file in the project root containing a valid API key.";
        return;
    }

    if ((Test-Path "$projectLocation\.symbolSourceApiKey") -eq $false) {
        Write-Host "SymbolSource API key not found. Create a '.symbolsourceApiKey' file in the project root containing a valid API key.";
        return;
    }

    $nugetApiKey = [IO.File]::ReadAllText("$projectLocation\.nugetApiKey");
    $symbolSourceApiKey = [IO.File]::ReadAllText("$projectLocation\.symbolSourceApiKey");

    if ($nugetApiKey.length -le 0) {
        Write-Host "Nuget API key file is empty.";
        return;
    }

    if ($symbolSourceApiKey.length -le 0) {
        Write-Host "SymbolSource API key file is empty.";
        return;
    }

    if (Test-Path -Path $projectLocation\GovDelivery.Nuget\bin\Release) {
        Remove-Item $projectLocation\GovDelivery.Nuget\bin\Release -Force -Recurse;
    }

    Invoke-ReleaseBuild;

    $latestNugetPackageFile = Get-ChildItem `
		-Path "$projectLocation\GovDelivery.Nuget\bin\Release\" `
        -Include "*.nupkg" `
        -Exclude "*symbols.nupkg" `
        -Name `
    | Select-Object -Last 1;
    
    $latestSymbolPackageFile =  Get-ChildItem `
		-Path "$projectLocation\GovDelivery.Nuget\bin\Release\" `
		-Filter "CityofNewOrleans.GovDeliveryApiService.*symbols.nupkg" `
        -Name `
    | Select-Object -Last 1;

    Write-Host "Found Nuget Package: $latestNugetPackageFile";

    Write-Host "Found Symbol Package: $latestSymbolPackageFile";

    & dotnet nuget push $latestNugetPackageFile `
        --source https://api.nuget.org/v3/index.json `
        --api-key $nugetApiKey `
        --symbol-api-key $symbolSourceApiKey
        --symbol-source https://www.myget.org/F/cityofneworleans/symbols/api/v2/package;

}

Export-ModuleMember -Function Test-Library, Publish-ConsoleApp, Invoke-ConsoleApp, Test-ConsoleApp, Initialize-ConsoleAppDb, Invoke-ReleaseBuild, Push-Nuget;
