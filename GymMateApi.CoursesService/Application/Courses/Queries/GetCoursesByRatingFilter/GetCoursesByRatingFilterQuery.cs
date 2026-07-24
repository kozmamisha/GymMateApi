using GymMateApi.Application.Dto;
using MediatR;

namespace GymMateApi.Application.Courses.Queries.GetCoursesByRatingFilter;

public record GetCoursesByRatingFilterQuery(int Rating) : IRequest<List<CourseDto>>;