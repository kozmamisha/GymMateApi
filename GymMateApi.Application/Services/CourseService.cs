using System.Security.Claims;
using GymMateApi.Application.Dto;
using GymMateApi.Application.Exceptions;
using GymMateApi.Application.Extensions;
using GymMateApi.Application.Interfaces;
using GymMateApi.Core.Entities;
using GymMateApi.Infrastructure.Auth;
using GymMateApi.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GymMateApi.Application.Services;

public class CourseService(
    ICourseRepository courseRepository, 
    ITrainingRepository trainingRepository,
    IUserRepository userRepository,
    IHttpContextAccessor httpContextAccessor) : ICourseService
{
    private Guid CurrentUserId => Guid.Parse(
        httpContextAccessor.HttpContext!.User.FindFirstValue(CustomClaims.UserId)!);
    
    public async Task<List<CourseDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var courses = await courseRepository.GetAllCourses(cancellationToken);

        return courses.ToDtoList();
    }

    public async Task<CourseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetCourseById(id, cancellationToken) 
                     ?? throw new EntityNotFoundException("Course not found");
        
        return course.ToDto();
    }

    public async Task CreateAsync(string name, CancellationToken cancellationToken)
    {
        CourseEntity course = new()
        {
            Name = name
        };
        
        await courseRepository.CreateCourse(course, cancellationToken);
    }

    public async Task UpdateAsync(Guid id, string name, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BadRequestException("Course name cannot be empty");
        
        var course = await courseRepository.GetCourseById(id, cancellationToken) 
                     ?? throw new EntityNotFoundException("Course not found");
        
        course.Name = name;
        
        await courseRepository.UpdateCourse(course, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetCourseById(id, cancellationToken) 
                     ?? throw new EntityNotFoundException("Course not found");
        
        await courseRepository.DeleteCourse(course, cancellationToken);
    }

    public async Task AddTrainingToCourseAsync(Guid courseId, Guid trainingId, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetCourseById(courseId, cancellationToken) 
                     ?? throw new EntityNotFoundException("Course not found");
        
        var training = await trainingRepository.GetTrainingById(trainingId, cancellationToken) 
                     ?? throw new EntityNotFoundException("Training not found");
        
        var trainingAlreadyAdded = course.Trainings.Any(t => t.Id == trainingId);
        if (trainingAlreadyAdded)
            throw new BadRequestException("This training is already added to this course");

        await courseRepository.AddTrainingToCourse(courseId, trainingId, cancellationToken);
    }    
    
    public async Task RemoveTrainingFromCourseAsync(Guid courseId, Guid trainingId, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetCourseById(courseId, cancellationToken) 
                     ?? throw new EntityNotFoundException("Course not found");

        var training = await trainingRepository.GetTrainingById(trainingId, cancellationToken)
                       ?? throw new EntityNotFoundException("Training not found");
        
        var trainingNotFound = course.Trainings.Any(t => t.Id == trainingId);
        if (!trainingNotFound)
            throw new EntityNotFoundException("There is no this training in this course");

        await courseRepository.RemoveTrainingFromCourse(courseId, trainingId, cancellationToken);
    }

    public async Task RateCourseAsync(Guid courseId, int rating, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetCourseById(courseId, cancellationToken) 
                     ?? throw new EntityNotFoundException("Course not found");
        
        if (rating < 1 || rating > 5)
            throw new BadRequestException("Rating must be between 1 and 5.");
        
        await courseRepository.RateCourse(courseId, rating, cancellationToken);
    }

    public async Task<List<CourseDto>> GetCoursesByRatingFilterAsync(int rating, CancellationToken cancellationToken)
    {
        if (rating < 1 || rating > 5)
            throw new BadRequestException("Rating must be between 1 and 5.");

        var filteredCourses = await courseRepository.GetCoursesByRatingFilter(rating, cancellationToken);
        
        return filteredCourses.ToDtoList();
    }

    public async Task<List<CourseDto>> GetCoursesSortedByRatingAsync(bool isDescending, CancellationToken cancellationToken)
    {
        var sortedCourses = await courseRepository.GetCoursesSortedByRating(isDescending, cancellationToken);
        
        return sortedCourses.ToDtoList();
    }

    public async Task SubscribeToCourse(Guid courseId, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetCourseById(courseId, cancellationToken) 
                     ?? throw new EntityNotFoundException("Course not found");
        
        var alreadySubscribed = course.Subscribers.Any(t => t.Id == CurrentUserId);
        if (alreadySubscribed)
            throw new BadRequestException("This user is already subscribed to this course");

        await courseRepository.SubscribeToCourse(courseId, CurrentUserId, cancellationToken);
    }

    public async Task UnsubscribeFromCourse(Guid courseId, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetCourseById(courseId, cancellationToken) 
                     ?? throw new EntityNotFoundException("Course not found");
        
        await courseRepository.UnsubscribeFromCourse(courseId, CurrentUserId, cancellationToken);
    }
}