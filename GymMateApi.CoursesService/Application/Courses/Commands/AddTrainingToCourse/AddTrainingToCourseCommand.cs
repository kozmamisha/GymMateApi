using MediatR;

namespace GymMateApi.Application.Courses.Commands.AddTrainingToCourse;

public record AddTrainingToCourseCommand(Guid CourseId, Guid TrainingId) : IRequest;