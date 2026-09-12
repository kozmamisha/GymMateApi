using GymMateApi.CoursesService.Persistance.Interfaces;
using GymMateApi.Shared.Exceptions;
using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Commands.AddTrainingToCourse;

public class AddTrainingToCourseHandler(
    ICourseRepository courseRepository) : IRequestHandler<AddTrainingToCourseCommand>
{
    public async Task Handle(AddTrainingToCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetCourseById(request.CourseId, cancellationToken)
                     ?? throw new EntityNotFoundException("Course not found");

        var alreadyAdded = course.TrainingIds.Any(t => t == request.TrainingId);
        if (alreadyAdded)
            throw new BadRequestException("This training is already added to this course");

        await courseRepository.AddTrainingToCourse(request.CourseId, request.TrainingId, cancellationToken);
    }
}
