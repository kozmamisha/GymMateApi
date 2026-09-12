using GymMateApi.ExercisesService.Application.Dto;
using GymMateApi.ExercisesService.Core;

namespace GymMateApi.ExercisesService.Application.Extensions;

public static class ExerciseMappingExtension
{
    public static ExerciseDto ToDto(this ExerciseEntity exercise)
    {
        return new ExerciseDto
        {
            Id = exercise.Id,
            Name = exercise.Name,
            Description = exercise.Description,
            TrainingId = exercise.TrainingId
        };
    }

    public static List<ExerciseDto> ToDtoList(this IEnumerable<ExerciseEntity> exercises)
    {
        return exercises.Select(e => e.ToDto()).ToList();
    }
}
