using MediatR;

namespace GymMateApi.Application.Courses.Commands.UnsubscribeFromCourse;

public record UnsubscribeFromCourseCommand(Guid CourseId, Guid UserId) : IRequest;