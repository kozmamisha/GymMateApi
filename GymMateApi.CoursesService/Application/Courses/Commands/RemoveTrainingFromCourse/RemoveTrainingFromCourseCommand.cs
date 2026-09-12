using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Commands.RemoveTrainingFromCourse;

public record RemoveTrainingFromCourseCommand(Guid CourseId, Guid TrainingId) : IRequest;
