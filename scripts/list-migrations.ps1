<#
.SYNOPSIS
    Lists migrations per service and marks the ones already applied to the database.
.EXAMPLE
    ./scripts/list-migrations.ps1
    ./scripts/list-migrations.ps1 -Service Auth
#>
param([string]$Service)

. "$PSScriptRoot/_common.ps1"

foreach ($project in Get-TargetServices $Service) {
    Write-Host "`n=== $project ===" -ForegroundColor Magenta
    Invoke-Ef -EfArgs @('migrations', 'list') -Project $project
}
