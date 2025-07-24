using GymMateApi.Application.Dto;
using GymMateApi.Application.Exceptions;
using GymMateApi.Application.Extensions;
using GymMateApi.Application.Interfaces;
using GymMateApi.Core.Entities;
using GymMateApi.Persistence.Interfaces;

namespace GymMateApi.Application.Services;

public class CourseService(ICourseRepository courseRepository) : ICourseService
{
    public async Task<List<CourseDto>> GetAllAsync()
    {
        var courses = await courseRepository.GetAllCourses();

        return courses.ToDtoList();
    }

    public async Task<CourseDto?> GetByIdAsync(Guid id)
    {
        var course = await courseRepository.GetCourseById(id) 
                     ?? throw new EntityNotFoundException("Course not found");
        
        return course.ToDto();
    }

    public async Task CreateAsync(string name)
    {
        CourseEntity course = new()
        {
            Name = name
        };
        
        await courseRepository.CreateCourse(course);
    }

    public async Task UpdateAsync(Guid id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BadRequestException("Course name cannot be empty");
        
        var course = await courseRepository.GetCourseById(id) 
                     ?? throw new EntityNotFoundException("Course not found");
        
        course.Name = name;
        
        await courseRepository.UpdateCourse(course);
    }

    public async Task DeleteAsync(Guid id)
    {
        var course = await courseRepository.GetCourseById(id) 
                     ?? throw new EntityNotFoundException("Course not found");
        
        await courseRepository.DeleteCourse(course);
    }
}