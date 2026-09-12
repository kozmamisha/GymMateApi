using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Commands.UnsubscribeFromCourse;

public record UnsubscribeFromCourseCommand(Guid CourseId, Guid UserId) : IRequest;
