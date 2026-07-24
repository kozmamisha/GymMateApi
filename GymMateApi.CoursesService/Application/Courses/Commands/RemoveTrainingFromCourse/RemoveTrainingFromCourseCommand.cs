using MediatR;

namespace GymMateApi.Application.Courses.Commands.RemoveTrainingFromCourse;

public record RemoveTrainingFromCourseCommand(Guid CourseId, Guid TrainingId) : IRequest;