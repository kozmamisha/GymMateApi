using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Commands.SubscribeToCourse;

public record SubscribeToCourseCommand(Guid CourseId, Guid UserId) : IRequest;
