using GymMateApi.Application.Interfaces;
using GymMateApi.Contracts.Course;
using Microsoft.AspNetCore.Mvc;

namespace GymMateApi.Controllers;

[ApiController]
[Route("api/courses")]
public class CourseController(ICourseService courseService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateCourse([FromBody] CourseUpsertRequest request, CancellationToken cancellationToken)
    {
        await courseService.CreateAsync(request.Name, cancellationToken);
        return Ok();
    }

    [HttpPost("{courseId:guid}/trainings")]
    public async Task<ActionResult> AddTrainingToCourseAsync(
        [FromRoute] Guid courseId,
        [FromBody] CourseTrainingRequest request,
        CancellationToken cancellationToken)
    {
        await courseService.AddTrainingToCourseAsync(courseId, request.TrainingId, cancellationToken);
        return Ok();
    }
    
    [HttpDelete("{courseId:guid}/trainings")]
    public async Task<ActionResult> RemoveTrainingFromCourseAsync(
        [FromRoute] Guid courseId,
        [FromBody] CourseTrainingRequest request,
        CancellationToken cancellationToken)
    {
        await courseService.RemoveTrainingFromCourseAsync(courseId, request.TrainingId, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/rate")]
    public async Task<ActionResult> RateCourseAsync([FromRoute] Guid id, [FromBody] int rating, CancellationToken cancellationToken)
    {
        await courseService.RateCourseAsync(id, rating, cancellationToken);
        return Ok();
    }

    [HttpGet()]
    public async Task<ActionResult> GetAllCourses(CancellationToken cancellationToken)
    {
        var courses = await courseService.GetAllAsync(cancellationToken);
        return Ok(courses);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetOneCourse([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var course = await courseService.GetByIdAsync(id, cancellationToken);
        return Ok(course);
    }

    [HttpGet("filtered")]
    public async Task<ActionResult> GetCoursesByRatingFilter([FromQuery] int rating, CancellationToken cancellationToken)
    {
        var courses = await courseService.GetCoursesByRatingFilterAsync(rating, cancellationToken);
        return Ok(courses);
    }    
    
    [HttpGet("sorted")]
    public async Task<ActionResult> GetCoursesSortedByRating([FromQuery] bool isDescending, CancellationToken cancellationToken)
    {
        var courses = await courseService.GetCoursesSortedByRatingAsync(isDescending, cancellationToken);
        return Ok(courses);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateCourse([FromRoute] Guid id, [FromBody] CourseUpsertRequest request, CancellationToken cancellationToken)
    {
        await courseService.UpdateAsync(id, request.Name, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteCourse([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await courseService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}