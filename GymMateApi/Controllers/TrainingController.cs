using GymMateApi.Application.Interfaces;
using GymMateApi.Contracts.Training;
using Microsoft.AspNetCore.Mvc;

namespace GymMateApi.Controllers
{
    [ApiController]
    [Route("api/trainings")]
    public class TrainingController(ITrainingService trainingService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateTraining([FromBody] TrainingUpsertRequest request, CancellationToken cancellationToken)
        {
            await trainingService.CreateAsync(request.Name, request.Description, cancellationToken);
            return Ok();
        }

        [HttpGet()]
        public async Task<ActionResult> GetAllTrainings(CancellationToken cancellationToken)
        {
            var trainings = await trainingService.GetAllAsync(cancellationToken);
            return Ok(trainings);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetOneTraining([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var training = await trainingService.GetByIdAsync(id, cancellationToken);
            return Ok(training);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateTraining([FromRoute] Guid id, [FromBody] TrainingUpsertRequest request, CancellationToken cancellationToken)
        {
            await trainingService.UpdateAsync(id, request.Name, request.Description, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteTraining([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await trainingService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
