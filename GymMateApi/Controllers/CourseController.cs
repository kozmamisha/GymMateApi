using GymMateApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymMateApi.Controllers;

[ApiController]
[Route("api/courses")]
public class CourseController(ICourseService courseService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateCourse([FromBody] string name, CancellationToken cancellationToken)
    {
        await courseService.CreateAsync(name, cancellationToken);
        return Ok();
    }

    [HttpPost("{courseId:guid}/trainings")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> AddTrainingToCourseAsync(
        [FromRoute] Guid courseId,
        [FromBody] Guid trainingId,
        CancellationToken cancellationToken)
    {
        await courseService.AddTrainingToCourseAsync(courseId, trainingId, cancellationToken);
        return Ok();
    }
    
    [HttpDelete("{courseId:guid}/trainings")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> RemoveTrainingFromCourseAsync(
        [FromRoute] Guid courseId,
        [FromBody] Guid trainingId,
        CancellationToken cancellationToken)
    {
        await courseService.RemoveTrainingFromCourseAsync(courseId, trainingId, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/rate")]
    [Authorize]
    public async Task<ActionResult> RateCourseAsync([FromRoute] Guid id, [FromBody] int rating, CancellationToken cancellationToken)
    {
        await courseService.RateCourseAsync(id, rating, cancellationToken);
        return Ok();
    }

    [HttpGet()]
    [Authorize]
    public async Task<ActionResult> GetAllCourses(CancellationToken cancellationToken)
    {
        var courses = await courseService.GetAllAsync(cancellationToken);
        return Ok(courses);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult> GetOneCourse([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var course = await courseService.GetByIdAsync(id, cancellationToken);
        return Ok(course);
    }

    [HttpGet("filtered")]
    [Authorize]
    public async Task<ActionResult> GetCoursesByRatingFilter([FromQuery] int rating, CancellationToken cancellationToken)
    {
        var courses = await courseService.GetCoursesByRatingFilterAsync(rating, cancellationToken);
        return Ok(courses);
    }    
    
    [HttpGet("sorted")]
    [Authorize]
    public async Task<ActionResult> GetCoursesSortedByRating([FromQuery] bool isDescending, CancellationToken cancellationToken)
    {
        var courses = await courseService.GetCoursesSortedByRatingAsync(isDescending, cancellationToken);
        return Ok(courses);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateCourse([FromRoute] Guid id, [FromBody] string name, CancellationToken cancellationToken)
    {
        await courseService.UpdateAsync(id, name, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteCourse([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await courseService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/subscribe")]
    [Authorize]
    public async Task<ActionResult> SubscribeToCourse([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await courseService.SubscribeToCourse(id, cancellationToken);
        return Ok();
    }    
    
    [HttpDelete("{id:guid}/unsubscribe")]
    [Authorize]
    public async Task<ActionResult> UnsubscribeFromCourse([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await courseService.UnsubscribeFromCourse(id, cancellationToken);
        return NoContent();
    }
}