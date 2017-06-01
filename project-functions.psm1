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
    & dotnet publish .\GovDelivery.ConsoleApp\GovDelivery.ConsoleApp.csproj -c release -r win7-x64;
}

function Invoke-ConsoleApp {

    #& dotnet run -p .\GovDelivery.ConsoleApp\GovDelivery.ConsoleApp.csproj -- $args;
    & .\GovDelivery.ConsoleApp\bin\release\netcoreapp1.1\win7-x64\GovDelivery.ConsoleApp.exe $args;

}

function Test-Library {
    & dotnet test .\GovDelivery.Tests\GovDelivery.Tests.csproj;
}

#function Test-ConsoleApp {
#    Test-BuiltProject {
#        & .\packages\xunit.runner.console.2.2.0\tools\xunit.console.exe .\GovDelivery.Tests\bin\Debug\GovDelivery.Example.Tests.dll -verbose
#    };
#}

Export-ModuleMember -Function Test-Library, Publish-ConsoleApp, Invoke-ConsoleApp, Test-ConsoleApp;
