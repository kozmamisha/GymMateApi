using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Commands.RateCourse;

public record RateCourseCommand(Guid CourseId, int Rating) : IRequest;
