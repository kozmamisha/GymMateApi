using GymMateApi.CoursesService.Application.Dto;
using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Queries.GetAllCourses;

public record GetAllCoursesQuery() : IRequest<List<CourseDto>>;
