using GymMateApi.Application.Interfaces;
using GymMateApi.Contracts.Training;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymMateApi.Controllers
{
    [ApiController]
    [Route("api/trainings")]
    public class TrainingController(ITrainingService trainingService) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateTraining([FromBody] TrainingUpsertRequest request, CancellationToken cancellationToken)
        {
            await trainingService.CreateAsync(request.Name, request.Description, cancellationToken);
            return Ok();
        }

        [HttpGet()]
        [Authorize]
        public async Task<ActionResult> GetAllTrainings(CancellationToken cancellationToken)
        {
            var trainings = await trainingService.GetAllAsync(cancellationToken);
            return Ok(trainings);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult> GetOneTraining([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var training = await trainingService.GetByIdAsync(id, cancellationToken);
            return Ok(training);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateTraining([FromRoute] Guid id, [FromBody] TrainingUpsertRequest request, CancellationToken cancellationToken)
        {
            await trainingService.UpdateAsync(id, request.Name, request.Description, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteTraining([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await trainingService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
