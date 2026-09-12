using GymMateApi.TrainingsService.Application.Dto;
using GymMateApi.TrainingsService.Core;

namespace GymMateApi.TrainingsService.Application.Extensions;

public static class TrainingMappingExtension
{
    public static TrainingDto ToDto(this TrainingEntity training)
    {
        return new TrainingDto
        {
            Id = training.Id,
            Name = training.Name,
            Description = training.Description
        };
    }

    public static List<TrainingDto> ToDtoList(this IEnumerable<TrainingEntity> trainings)
    {
        return trainings.Select(t => t.ToDto()).ToList();
    }
}
