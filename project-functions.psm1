function Test-Project () {
    Write-Host "Building project..."
    & MSBuild /verbosity:quiet

    if ($LastExitCode -eq 0) {
        Write-Host "Project build successful. Running tests now.."
        & .\packages\xunit.runner.console.2.2.0\tools\xunit.console.exe .\GovDelivery.Tests\bin\Debug\GovDelivery.Tests.dll -verbose
    }
    else {
        Write-Host "Build Failed. Aborting test." -ForegroundColor Red
    }

}

Export-ModuleMember -Function Test-Project