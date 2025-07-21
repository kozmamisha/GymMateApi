using GymMateApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Application.Dto
{
    public class CommentDto
    {
        public string Text { get; set; } = string.Empty;

        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; } = string.Empty;

        public Guid TrainingId { get; set; }
    }
}
