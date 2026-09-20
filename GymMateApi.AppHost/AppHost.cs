var builder = DistributedApplication.CreateBuilder(args);

var jwtSecret = builder.AddParameter("jwt-secret", secret: true);
var postgresPassword = builder.AddParameter("postgres-password", secret: true);

var postgres = builder.AddPostgres("postgres", password: postgresPassword)
    .WithDataVolume("gymmate-pgdata")
    .WithHostPort(5432);

var authDb = postgres.AddDatabase("AuthDbContext", "gymMate_auth");
var commentsDb = postgres.AddDatabase("CommentsDbContext", "gymMate_comments");
var coursesDb = postgres.AddDatabase("CoursesDbContext", "gymMate_courses");
var exercisesDb = postgres.AddDatabase("ExercisesDbContext", "gymMate_exercises");
var trainingsDb = postgres.AddDatabase("TrainingsDbContext", "gymMate_trainings");

builder.AddProject<Projects.GymMateApi_AuthService>("auth")
    .WithReference(authDb)
    .WaitFor(authDb)
    .WithEnvironment("JwtOptions__SecretKey", jwtSecret);

builder.AddProject<Projects.GymMateApi_CommentsService>("comments")
    .WithReference(commentsDb)
    .WaitFor(commentsDb)
    .WithEnvironment("JwtOptions__SecretKey", jwtSecret);

builder.AddProject<Projects.GymMateApi_CoursesService>("courses")
    .WithReference(coursesDb)
    .WaitFor(coursesDb)
    .WithEnvironment("JwtOptions__SecretKey", jwtSecret);

builder.AddProject<Projects.GymMateApi_ExercisesService>("exercises")
    .WithReference(exercisesDb)
    .WaitFor(exercisesDb)
    .WithEnvironment("JwtOptions__SecretKey", jwtSecret);

builder.AddProject<Projects.GymMateApi_TrainingsService>("trainings")
    .WithReference(trainingsDb)
    .WaitFor(trainingsDb)
    .WithEnvironment("JwtOptions__SecretKey", jwtSecret);

builder.Build().Run();
