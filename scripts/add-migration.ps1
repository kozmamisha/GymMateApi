<#
.SYNOPSIS
    Adds an EF Core migration to ONE service.
.DESCRIPTION
    -Service is required on purpose: a schema change belongs to the single service
    that owns that database. Never add the same migration to every service.
.EXAMPLE
    ./scripts/add-migration.ps1 -Service Courses -Name AddCourseDescription
#>
param(
    [Parameter(Mandatory)][string]$Service,
    [Parameter(Mandatory)][string]$Name
)

. "$PSScriptRoot/_common.ps1"

$project = (Get-TargetServices $Service)[0]
Invoke-Ef -EfArgs @('migrations', 'add', $Name, '-o', 'Persistance/Migrations') -Project $project

Write-Host "Migration '$Name' added to $project." -ForegroundColor Green
Write-Host "Review it, then run: ./scripts/update-database.ps1 -Service $Service" -ForegroundColor Yellow
