using GymMateApi.Application.Dto;
using MediatR;

namespace GymMateApi.Application.Courses.Queries.GetAllCourses;

public record GetAllCoursesQuery() : IRequest<List<CourseDto>>;