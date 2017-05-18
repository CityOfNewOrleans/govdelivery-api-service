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

function Test-Library {
    Test-BuiltProject {
        & dotnet test .\GovDelivery.Tests\GovDelivery.Tests.csproj
    };
}

#function Test-ConsoleApp {
#    Test-BuiltProject {
#        & .\packages\xunit.runner.console.2.2.0\tools\xunit.console.exe .\GovDelivery.Tests\bin\Debug\GovDelivery.Example.Tests.dll -verbose
#    };
#}

Export-ModuleMember -Function Test-Library, Test-ConsoleApp;
