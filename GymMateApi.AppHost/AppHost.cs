var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.GymMateApi_AuthService>("gymmateapi-authservice");

builder.AddProject<Projects.GymMateApi_CommentsService>("gymmateapi-commentsservice");

builder.AddProject<Projects.GymMateApi_CoursesService>("gymmateapi-coursesservice");

builder.AddProject<Projects.GymMateApi_ExercisesService>("gymmateapi-exercisesservice");

builder.AddProject<Projects.GymMateApi_TrainingsService>("gymmateapi-trainingsservice");

builder.Build().Run();
