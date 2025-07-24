using GymMateApi.Application.Dto;
using GymMateApi.Core.Entities;

namespace GymMateApi.Application.Extensions;

public static class ExerciseMappingExtension
{
    public static ExerciseDto ToDto(this ExerciseEntity exercise)
    {
        return new ExerciseDto
        {
            Id = exercise.Id,
            Name = exercise.Name,
            Description = exercise.Description,
        };
    }

    public static List<ExerciseDto> ToDtoList(this IEnumerable<ExerciseEntity> exercises)
    {
        return exercises.Select(t => t.ToDto()).ToList();
    }
}