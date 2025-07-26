using GymMateApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymMateApi.Controllers
{
    [ApiController]
    [Route("api/trainings")]
    public class TrainingController(ITrainingService trainingService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult> CreateTraining(string name, string description)
        {
            await trainingService.CreateAsync(name, description);
            return Ok();
        }

        [HttpGet()]
        public async Task<ActionResult> GetAllTrainings()
        {
            var trainings = await trainingService.GetAllAsync();
            return Ok(trainings);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetOneTraining([FromRoute] Guid id)
        {
            var training = await trainingService.GetByIdAsync(id);
            return Ok(training);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateTraining([FromRoute] Guid id, string name, string description)
        {
            await trainingService.UpdateAsync(id, name, description);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteTraining([FromRoute] Guid id)
        {
            await trainingService.DeleteAsync(id);
            return NoContent();
        }
    }
}
