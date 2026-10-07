using GymMateApi.AuthService.Persistance;
using GymMateApi.CommentsService.Persistance;
using GymMateApi.CoursesService.Persistance;
using GymMateApi.ExercisesService.Persistance;
using GymMateApi.MigrationService;
using GymMateApi.ServiceDefaults;
using GymMateApi.TrainingsService.Persistance;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<AuthDbContext>("AuthDbContext");
builder.AddNpgsqlDbContext<CommentsDbContext>("CommentsDbContext");
builder.AddNpgsqlDbContext<CoursesDbContext>("CoursesDbContext");
builder.AddNpgsqlDbContext<ExercisesDbContext>("ExercisesDbContext");
builder.AddNpgsqlDbContext<TrainingsDbContext>("TrainingsDbContext");

builder.Services.AddHostedService<MigrationWorker>();

builder.Build().Run();
