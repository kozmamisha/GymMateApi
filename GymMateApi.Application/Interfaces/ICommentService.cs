﻿using GymMateApi.Application.Dto;
using GymMateApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Application.Interfaces
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAllAsync();
        Task<List<CommentDto>> GetByPageAsync(int page, int pageSize);
        Task CreateAsync(string text, Guid authorId, Guid trainingId);
        Task UpdateAsync(Guid id, string text);
        Task DeleteAsync(Guid id);
    }
}
