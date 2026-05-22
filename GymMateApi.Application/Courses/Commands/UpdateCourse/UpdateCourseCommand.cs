using MediatR;

namespace GymMateApi.Application.Courses.Commands.UpdateCourse;

public record UpdateCourseCommand(Guid Id, string Name) : IRequest;