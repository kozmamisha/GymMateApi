# Shared helpers for the GymMate maintenance scripts.
# Dot-sourced by the other scripts in this folder - not meant to be run directly.

$ErrorActionPreference = 'Stop'

# Short name -> project folder. Add new services here only.
$script:Services = [ordered]@{
    Auth      = 'GymMateApi.AuthService'
    Comments  = 'GymMateApi.CommentsService'
    Courses   = 'GymMateApi.CoursesService'
    Exercises = 'GymMateApi.ExercisesService'
    Trainings = 'GymMateApi.TrainingsService'
}

$script:RepoRoot = Split-Path -Parent $PSScriptRoot

function Get-TargetServices {
    param([string]$Service)

    if ([string]::IsNullOrWhiteSpace($Service)) {
        return @($script:Services.Values)
    }

    $key = $script:Services.Keys | Where-Object { $_ -ieq $Service }
    if (-not $key) {
        throw "Unknown service '$Service'. Known: $($script:Services.Keys -join ', ')"
    }

    return @($script:Services[$key])
}

function Invoke-Ef {
    param(
        [Parameter(Mandatory)][string[]]$EfArgs,
        [Parameter(Mandatory)][string]$Project
    )

    Push-Location $script:RepoRoot
    try {
        Write-Host "-> dotnet ef $($EfArgs -join ' ') --project $Project" -ForegroundColor Cyan
        dotnet ef @EfArgs --project $Project
        if ($LASTEXITCODE -ne 0) { throw "dotnet ef failed for $Project" }
    }
    finally {
        Pop-Location
    }
}
