<#
.SYNOPSIS
    DESTRUCTIVE. Drops the service databases and recreates them from migrations.
.DESCRIPTION
    Local development only - every row is deleted. Requires -Force to actually run.
.EXAMPLE
    ./scripts/reset-databases.ps1 -Force
    ./scripts/reset-databases.ps1 -Service Comments -Force
#>
param(
    [string]$Service,
    [switch]$Force
)

. "$PSScriptRoot/_common.ps1"

$targets = Get-TargetServices $Service

if (-not $Force) {
    Write-Host "This DROPS the database of: $($targets -join ', ')" -ForegroundColor Red
    Write-Host "All data will be lost. Re-run with -Force if that is what you want." -ForegroundColor Red
    exit 1
}

foreach ($project in $targets) {
    Invoke-Ef -EfArgs @('database', 'drop', '--force') -Project $project
    Invoke-Ef -EfArgs @('database', 'update') -Project $project
}

Write-Host "Databases recreated from scratch." -ForegroundColor Green
