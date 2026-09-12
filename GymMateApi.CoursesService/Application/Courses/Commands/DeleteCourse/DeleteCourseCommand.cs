using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Commands.DeleteCourse;

public record DeleteCourseCommand(Guid Id) : IRequest;
