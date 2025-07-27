using GymMateApi.Application.Dto;
using GymMateApi.Application.Exceptions;
using GymMateApi.Application.Extensions;
using GymMateApi.Application.Interfaces;
using GymMateApi.Core.Entities;
using GymMateApi.Persistence.Interfaces;

namespace GymMateApi.Application.Services;

public class CourseService(
    ICourseRepository courseRepository, 
    ITrainingRepository trainingRepository) : ICourseService
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

    public async Task AddTrainingToCourseAsync(Guid courseId, Guid trainingId)
    {
        var course = await courseRepository.GetCourseById(courseId) 
                     ?? throw new EntityNotFoundException("Course not found");
        
        var training = await trainingRepository.GetTrainingById(trainingId) 
                     ?? throw new EntityNotFoundException("Training not found");
        
        var trainingAlreadyAdded = course.Trainings.Any(t => t.Id == trainingId);
        if (trainingAlreadyAdded)
            throw new BadRequestException("This training is already added to this course");

        await courseRepository.AddTrainingToCourse(courseId, trainingId);
    }    
    
    public async Task RemoveTrainingFromCourseAsync(Guid courseId, Guid trainingId)
    {
        var course = await courseRepository.GetCourseById(courseId) 
                     ?? throw new EntityNotFoundException("Course not found");

        var training = await trainingRepository.GetTrainingById(trainingId)
                       ?? throw new EntityNotFoundException("Training not found");

        await courseRepository.RemoveTrainingFromCourse(courseId, trainingId);
    }
}