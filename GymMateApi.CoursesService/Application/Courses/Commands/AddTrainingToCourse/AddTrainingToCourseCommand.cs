using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Commands.AddTrainingToCourse;

public record AddTrainingToCourseCommand(Guid CourseId, Guid TrainingId) : IRequest;
