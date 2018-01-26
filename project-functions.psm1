$solutionPath = Get-Location;

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
    & dotnet publish $solutionPath\GovDelivery.ConsoleApp\GovDelivery.ConsoleApp.csproj -c release -r win7-x64;
}

function Invoke-PackNugetRelease {
    Param([parameter(Mandatory=$true)][String]$ProjectName)

    & dotnet pack -c "Release" $solutionPath\$ProjectName --include-symbols --include-source
}

function Invoke-ConsoleApp {

    & $solutionPath\GovDelivery.ConsoleApp\bin\release\netcoreapp1.1\win7-x64\GovDelivery.ConsoleApp.exe $args;

}

function Test-Library {
    & dotnet test $solutionPath\GovDelivery.Tests\GovDelivery.Tests.csproj;
}

function Find-NugetApiKeys {

    if ((Test-Path "$solutionPath\.nugetApiKey") -eq $false) {
        Write-Host "Nuget API key not found. Create a '.nugetApiKey' file in the project root containing a valid API key.";
        return;
    }

    if ((Test-Path "$solutionPath\.symbolSourceApiKey") -eq $false) {
        Write-Host "SymbolSource API key not found. Create a '.symbolsourceApiKey' file in the project root containing a valid API key.";
        return;
    }

    $nugetApiKey = [IO.File]::ReadAllText("$solutionPath\.nugetApiKey");
    $symbolSourceApiKey = [IO.File]::ReadAllText("$solutionPath\.symbolSourceApiKey");

    if ($nugetApiKey.length -le 0) {
        Write-Host "Nuget API key file is empty.";
        return $null;
    }

    if ($symbolSourceApiKey.length -le 0) {
        Write-Host "SymbolSource API key file is empty.";
        return $null;
    }

    $keys = New-Object -TypeName PSObject;

    $keys | Add-Member -NotePropertyName NugetApiKey -NotePropertyValue $nugetApiKey;
    $keys | Add-Member -NotePropertyName SymbolSourceApiKey -NotePropertyValue $symbolSourceApiKey;

    return $keys;
}

function Clear-ProjectBinDirectory {
    Param([Parameter(Mandatory=$true)][String]$ProjectName)

    if (!(Test-Path -Path $solutionPath\$ProjectName\bin\Release)) 
    {
        return;
    }
    
    Remove-Item $solutionPath\$ProjectName\bin\Release -Force -Recurse;

}

function Publish-NugetRelease {
    
    $keys = Find-NugetApiKeys;

    if ($keys -eq $null) 
    {
        Write-Host "Nuget and/or MyGet Symbol nuget keys not found."
        return;
    }

    Clear-ProjectBinDirectory -ProjectName GovDelivery.Csv;
    Invoke-PackNugetRelease -ProjectName GovDelivery.Csv;
    Push-LatestNugetPackage -ProjectName GovDelivery.Csv -Keys $keys;

    Clear-ProjectBinDirectory -ProjectName GovDelivery.Entity;
    Invoke-PackNugetRelease -ProjectName GovDelivery.Entity;
    Push-LatestNugetPackage -ProjectName GovDelivery.Entity -Keys $keys;

    Clear-ProjectBinDirectory -ProjectName GovDelivery.Rest;
    Invoke-PackNugetRelease -ProjectName GovDelivery.Rest;
    Push-LatestNugetPackage -ProjectName GovDelivery.Rest -Keys $keys;

    Clear-ProjectBinDirectory -ProjectName GovDelivery.Logic;
    Invoke-PackNugetRelease -ProjectName GovDelivery.Logic;
    Push-LatestNugetPackage -ProjectName GovDelivery.Logic -Keys $keys;

    Clear-ProjectBinDirectory -ProjectName GovDelivery.Nuget;
    Invoke-PackNugetRelease -ProjectName GovDelivery.Nuget;
    Push-LatestNugetPackage -ProjectName GovDelivery.Nuget -Keys $keys;
}

function Push-LatestNugetPackage {
    Param(
        [Parameter(Mandatory=$true)][String]$ProjectName,

        [Parameter()][PSObject]$Keys = $false
    )

    if ($Keys -eq $null) { $Keys = Find-NugetApiKeys; }

    $latestNugetPackageFile = Get-ChildItem `
		-Path "$solutionPath\$ProjectName\bin\Release\" `
        -Include "*.nupkg" `
        -Exclude "*.symbols.nupkg" `
        -Name `
    | Select-Object -Last 1;

    Write-Host "Found Nuget Package: $latestNugetPackageFile";

    & dotnet nuget push $solutionPath\$ProjectName\bin\Release\$latestNugetPackageFile `
        --source https://api.nuget.org/v3/index.json `
        --symbol-source https://www.myget.org/F/cityofneworleans/symbols/api/v2/package `
        --api-key $Keys.NugetApiKey `
        --symbol-api-key $Keys.SymbolSourceApiKey;

}

Export-ModuleMember -Function Test-Library, Publish-ConsoleApp, Invoke-ConsoleApp, Test-ConsoleApp, Initialize-ConsoleAppDb, Invoke-ReleaseBuild, Publish-NugetRelease;
