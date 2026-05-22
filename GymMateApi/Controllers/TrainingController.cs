using GymMateApi.Application.Trainings.Commands.CreateTraining;
using GymMateApi.Application.Trainings.Commands.DeleteTraining;
using GymMateApi.Application.Trainings.Commands.UpdateTraining;
using GymMateApi.Application.Trainings.Queries.GetAllTrainings;
using GymMateApi.Application.Trainings.Queries.GetTrainingById;
using GymMateApi.Contracts.Training;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymMateApi.Controllers
{
    [ApiController]
    [Route("api/trainings")]
    public class TrainingController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateTraining([FromBody] TrainingUpsertRequest request, CancellationToken cancellationToken)
        {
            await mediator.Send(new CreateTrainingCommand(request.Name, request.Description), cancellationToken);
            return Ok();
        }

        [HttpGet()]
        [Authorize]
        public async Task<ActionResult> GetAllTrainings(CancellationToken cancellationToken)
        {
            var trainings = await mediator.Send(new GetAllTrainingsQuery(), cancellationToken);
            return Ok(trainings);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult> GetOneTraining([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var training = await mediator.Send(new GetTrainingByIdQuery(id), cancellationToken);
            return Ok(training);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateTraining([FromRoute] Guid id, [FromBody] TrainingUpsertRequest request, CancellationToken cancellationToken)
        {
            await mediator.Send(new UpdateTrainingCommand(id, request.Name, request.Description), cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteTraining([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteTrainingCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
