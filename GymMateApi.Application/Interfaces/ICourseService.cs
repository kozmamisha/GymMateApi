using GymMateApi.Application.Dto;

namespace GymMateApi.Application.Interfaces;

public interface ICourseService
{
    Task<List<CourseDto>> GetAllAsync();
    Task<CourseDto?> GetByIdAsync(Guid id);
    Task CreateAsync(string name);
    Task UpdateAsync(Guid id, string name);
    Task DeleteAsync(Guid id);
}