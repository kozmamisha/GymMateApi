using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymMateApi.Core.Constants;

namespace GymMateApi.Core.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = Roles.Admin;

        public ICollection<CommentEntity> Comments { get; set; } = [];

        public Guid? CourseId { get; set; }
        public CourseEntity? Course { get; set; }
    }
}
