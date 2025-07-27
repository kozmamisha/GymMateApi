using GymMateApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Application.Dto
{
    public class TrainingDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ICollection<CommentDto> Comments { get; set; } = [];

        public ICollection<ExerciseDto> Exercises { get; set; } = [];
    }
}
