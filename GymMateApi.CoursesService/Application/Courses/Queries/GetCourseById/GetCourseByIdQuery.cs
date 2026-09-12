using GymMateApi.CoursesService.Application.Dto;
using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Queries.GetCourseById;

public record GetCourseByIdQuery(Guid Id) : IRequest<CourseDto>;
