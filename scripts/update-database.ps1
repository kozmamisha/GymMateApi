<#
.SYNOPSIS
    Applies pending EF Core migrations. Creates the database if it does not exist.
.EXAMPLE
    ./scripts/update-database.ps1                  # all services
    ./scripts/update-database.ps1 -Service Courses # one service
#>
param([string]$Service)

. "$PSScriptRoot/_common.ps1"

foreach ($project in Get-TargetServices $Service) {
    Invoke-Ef -EfArgs @('database', 'update') -Project $project
}

Write-Host "Databases are up to date." -ForegroundColor Green
