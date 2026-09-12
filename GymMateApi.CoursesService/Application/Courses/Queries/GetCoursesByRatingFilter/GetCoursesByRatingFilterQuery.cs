using GymMateApi.CoursesService.Application.Dto;
using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Queries.GetCoursesByRatingFilter;

public record GetCoursesByRatingFilterQuery(int Rating) : IRequest<List<CourseDto>>;
