using GymMateApi.Application.Dto;
using MediatR;

namespace GymMateApi.Application.Courses.Queries.GetCoursesSortedByRating;

public record GetCoursesSortedByRatingQuery(bool IsDescending) : IRequest<List<CourseDto>>;