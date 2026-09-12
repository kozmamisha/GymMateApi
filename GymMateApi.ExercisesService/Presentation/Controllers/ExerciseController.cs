using GymMateApi.ExercisesService.Application.Exercises.Commands.CreateExercise;
using GymMateApi.ExercisesService.Application.Exercises.Commands.DeleteExercise;
using GymMateApi.ExercisesService.Application.Exercises.Commands.UpdateExercise;
using GymMateApi.ExercisesService.Application.Exercises.Queries.GetAllExercises;
using GymMateApi.ExercisesService.Application.Exercises.Queries.GetExerciseById;
using GymMateApi.ExercisesService.Application.Exercises.Queries.GetExerciseByPage;
using GymMateApi.ExercisesService.Presentation.Contracts.Exercise;
using GymMateApi.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymMateApi.ExercisesService.Presentation.Controllers;

[ApiController]
[Route("api/exercises")]
public class ExerciseController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> CreateExercise([FromBody] CreateExerciseCommand command,
        CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return Created();
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetAllExercises(CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetAllExercisesQuery(), cancellationToken));

    [HttpGet("pagination")]
    [Authorize]
    public async Task<ActionResult> GetExercisesByPage([FromQuery] int page, [FromQuery] int pageSize,
        CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetExercisesByPageQuery(page, pageSize), cancellationToken));

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult> GetOneExercise(Guid id, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetExerciseByIdQuery(id), cancellationToken));

    [HttpPut("{id:guid}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> UpdateExercise([FromRoute] Guid id, [FromBody] UpdateExerciseRequest request,
        CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateExerciseCommand(id, request.Name, request.Description), cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> DeleteExercise(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteExerciseCommand(id), cancellationToken);
        return NoContent();
    }
}
