using GymMateApi.AuthService.Persistance;
using GymMateApi.CommentsService.Persistance;
using GymMateApi.CoursesService.Persistance;
using GymMateApi.ExercisesService.Persistance;
using GymMateApi.TrainingsService.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GymMateApi.MigrationService;

public class MigrationWorker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime lifetime,
    ILogger<MigrationWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();

            await MigrateAsync<AuthDbContext>(scope, stoppingToken);
            await MigrateAsync<CommentsDbContext>(scope, stoppingToken);
            await MigrateAsync<CoursesDbContext>(scope, stoppingToken);
            await MigrateAsync<ExercisesDbContext>(scope, stoppingToken);
            await MigrateAsync<TrainingsDbContext>(scope, stoppingToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Migration failed");
            
            Environment.ExitCode = 1;
        }
        finally
        {
            lifetime.StopApplication();
        }
    }

    private async Task MigrateAsync<TContext>(IServiceScope scope, CancellationToken cancellationToken)
        where TContext : DbContext
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(token => dbContext.Database.MigrateAsync(token), cancellationToken);

        logger.LogInformation("{Database} is up to date", typeof(TContext).Name);
    }
}
