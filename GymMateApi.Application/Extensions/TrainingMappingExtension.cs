using GymMateApi.Application.Dto;
using GymMateApi.Core.Entities;

namespace GymMateApi.Application.Extensions
{
    public static class TrainingMappingExtension
    {
        public static TrainingDto ToDto(this TrainingEntity training)
        {
            return new TrainingDto
            {
                Id = training.Id,
                Name = training.Name,
                Description = training.Description,
                Exercises = training.Exercises.Select(e => new ExerciseDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description
                }).ToList(),
                Comments = training.Comments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    Text = c.Text,
                    AuthorId = c.AuthorId,
                    CreatedAt = c.CreatedAt
                }).ToList(),
            };
        }

        public static List<TrainingDto> ToDtoList(this IEnumerable<TrainingEntity> trainings)
        {
            return trainings.Select(t => t.ToDto()).ToList();
        }
    }
}
