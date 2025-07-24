using GymMateApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymMateApi.Controllers;

[ApiController]
[Route("api/course")]
public class CourseController(ICourseService courseService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult> CreateCourse(string name)
    {
        await courseService.CreateAsync(name);
        return Ok();
    }

    [HttpGet("courses")]
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