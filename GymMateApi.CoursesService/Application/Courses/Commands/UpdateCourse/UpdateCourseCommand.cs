using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Commands.UpdateCourse;

public record UpdateCourseCommand(Guid Id, string Name) : IRequest;
