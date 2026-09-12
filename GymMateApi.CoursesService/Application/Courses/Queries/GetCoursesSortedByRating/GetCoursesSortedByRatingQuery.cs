using GymMateApi.CoursesService.Application.Dto;
using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Queries.GetCoursesSortedByRating;

public record GetCoursesSortedByRatingQuery(bool IsDescending) : IRequest<List<CourseDto>>;
