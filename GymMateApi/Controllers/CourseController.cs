using System.Security.Claims;
using GymMateApi.Application.Courses.Commands.AddTrainingToCourse;
using GymMateApi.Application.Courses.Commands.CreateCourse;
using GymMateApi.Application.Courses.Commands.DeleteCourse;
using GymMateApi.Application.Courses.Commands.RateCourse;
using GymMateApi.Application.Courses.Commands.RemoveTrainingFromCourse;
using GymMateApi.Application.Courses.Commands.SubscribeToCourse;
using GymMateApi.Application.Courses.Commands.UnsubscribeFromCourse;
using GymMateApi.Application.Courses.Commands.UpdateCourse;
using GymMateApi.Application.Courses.Queries.GetAllCourses;
using GymMateApi.Application.Courses.Queries.GetCourseById;
using GymMateApi.Application.Courses.Queries.GetCoursesByRatingFilter;
using GymMateApi.Application.Courses.Queries.GetCoursesSortedByRating;
using GymMateApi.Contracts.Course;
using GymMateApi.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymMateApi.Controllers;

[ApiController]
[Route("api/courses")]
public class CourseController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateCourse([FromBody] CourseUpsertRequest request, CancellationToken cancellationToken)
    {
        await mediator.Send(new CreateCourseCommand(request.Name), cancellationToken);
        return Ok();
    }

    [HttpPost("{courseId:guid}/trainings")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> AddTrainingToCourseAsync(
        [FromRoute] Guid courseId,
        [FromBody] CourseTrainingRequest request,
        CancellationToken cancellationToken)
    {
        await mediator.Send(new AddTrainingToCourseCommand(courseId, request.TrainingId), cancellationToken);
        return Ok();
    }
    
    [HttpDelete("{courseId:guid}/trainings")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> RemoveTrainingFromCourseAsync(
        [FromRoute] Guid courseId,
        [FromBody] CourseTrainingRequest request,
        CancellationToken cancellationToken)
    {
        await mediator.Send(new RemoveTrainingFromCourseCommand(courseId, request.TrainingId), cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/rate")]
    [Authorize]
    public async Task<ActionResult> RateCourseAsync([FromRoute] Guid id, [FromBody] int rating, CancellationToken cancellationToken)
    {
        await mediator.Send(new RateCourseCommand(id, rating), cancellationToken);
        return Ok();
    }

    [HttpGet()]
    [Authorize]
    public async Task<ActionResult> GetAllCourses(CancellationToken cancellationToken)
    {
        var courses = await mediator.Send(new GetAllCoursesQuery(), cancellationToken);
        return Ok(courses);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult> GetOneCourse([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var course = await mediator.Send(new GetCourseByIdQuery(id), cancellationToken);
        return Ok(course);
    }

    [HttpGet("filtered")]
    [Authorize]
    public async Task<ActionResult> GetCoursesByRatingFilter([FromQuery] int rating, CancellationToken cancellationToken)
    {
        var courses = await mediator.Send(new GetCoursesByRatingFilterQuery(rating), cancellationToken);
        return Ok(courses);
    }    
    
    [HttpGet("sorted")]
    [Authorize]
    public async Task<ActionResult> GetCoursesSortedByRating([FromQuery] bool isDescending, CancellationToken cancellationToken)
    {
        var courses = await mediator.Send(new GetCoursesSortedByRatingQuery(isDescending), cancellationToken);
        return Ok(courses);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateCourse([FromRoute] Guid id, [FromBody] CourseUpsertRequest request, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateCourseCommand(id, request.Name), cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteCourse([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteCourseCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPost("{courseId:guid}/subscribe")]
    [Authorize]
    public async Task<ActionResult> SubscribeToCourse(Guid courseId, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(CustomClaims.UserId)!);
        await mediator.Send(new SubscribeToCourseCommand(courseId, userId), cancellationToken);
        return NoContent();
    }    
    
    [HttpDelete("{courseId:guid}/unsubscribe")]
    [Authorize]
    public async Task<ActionResult> UnsubscribeFromCourse(Guid courseId, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(CustomClaims.UserId)!);
        await mediator.Send(new UnsubscribeFromCourseCommand(courseId, userId), cancellationToken);
        return NoContent();
    }
}