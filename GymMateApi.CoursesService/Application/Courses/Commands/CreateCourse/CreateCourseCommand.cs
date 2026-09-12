using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Commands.CreateCourse;

public record CreateCourseCommand(string Name) : IRequest;
