using MediatR;

namespace GymMateApi.Application.Courses.Commands.RateCourse;

public record RateCourseCommand(Guid CourseId, int Rating) : IRequest;