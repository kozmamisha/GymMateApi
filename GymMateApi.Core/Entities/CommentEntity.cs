using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Core.Entities
{
    public class CommentEntity
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Guid AuthorId { get; set; }
        public UserEntity? Author { get; set; }

        public Guid TrainingId { get; set; }
        public TrainingEntity? Training { get; set; }
    }
}
