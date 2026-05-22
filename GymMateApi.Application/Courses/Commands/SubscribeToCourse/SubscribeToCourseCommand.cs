using MediatR;

namespace GymMateApi.Application.Courses.Commands.SubscribeToCourse;

public record SubscribeToCourseCommand(Guid CourseId, Guid UserId) : IRequest;