using GymMateApi.Application.Dto;
using MediatR;

namespace GymMateApi.Application.Courses.Queries.GetCourseById;

public record GetCourseByIdQuery(Guid Id) : IRequest<CourseDto>;