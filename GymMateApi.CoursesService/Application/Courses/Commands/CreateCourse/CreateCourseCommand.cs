using MediatR;

namespace GymMateApi.Application.Courses.Commands.CreateCourse;

public record CreateCourseCommand(string Name) : IRequest;