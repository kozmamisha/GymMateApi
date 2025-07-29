using GymMateApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymMateApi.Controllers;

[ApiController]
[Route("api/courses")]
public class CourseController(ICourseService courseService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult> CreateCourse(string name)
    {
        await courseService.CreateAsync(name);
        return Ok();
    }

    [HttpPost("add-training")]
    public async Task<ActionResult> AddTrainingToCourseAsync(
        [FromQuery] Guid courseId,
        [FromQuery] Guid trainingId)
    {
        await courseService.AddTrainingToCourseAsync(courseId, trainingId);
        return Ok();
    }
    
    [HttpDelete("remove-training")]
    public async Task<ActionResult> RemoveTrainingFromCourseAsync(
        [FromQuery] Guid courseId,
        [FromQuery] Guid trainingId)
    {
        await courseService.RemoveTrainingFromCourseAsync(courseId, trainingId);
        return NoContent();
    }

    [HttpPost("{id:guid}/rate")]
    public async Task<ActionResult> RateCourseAsync([FromRoute] Guid id, [FromBody] int rating)
    {
        await courseService.RateCourseAsync(id, rating);
        return Ok();
    }

    [HttpGet()]
    public async Task<ActionResult> GetAllCourses()
    {
        var courses = await courseService.GetAllAsync();
        return Ok(courses);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetOneCourse([FromRoute] Guid id)
    {
        var course = await courseService.GetByIdAsync(id);
        return Ok(course);
    }

    [HttpGet("filtered")]
    public async Task<ActionResult> GetCoursesByRatingFilter([FromQuery] int rating)
    {
        var courses = await courseService.GetCoursesByRatingFilterAsync(rating);
        return Ok(courses);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateCourse([FromRoute] Guid id, string name)
    {
        await courseService.UpdateAsync(id, name);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteCourse([FromRoute] Guid id)
    {
        await courseService.DeleteAsync(id);
        return NoContent();
    }
}