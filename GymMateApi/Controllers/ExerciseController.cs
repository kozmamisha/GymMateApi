using GymMateApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymMateApi.Controllers
{
    [ApiController]
    [Route("api/exercises")]
    public class ExerciseController(IExerciseService exerciseService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult> CreateExercise(string name, string description, Guid trainingId)
        {
            await exerciseService.CreateAsync(name, description, trainingId);
            return Ok();
        }

        [HttpGet()]
        public async Task<ActionResult> GetAllExercises()
        {
            var exercises = await exerciseService.GetAllAsync();
            return Ok(exercises);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetOneExercise([FromRoute] Guid id)
        {
            var exercise = await exerciseService.GetByIdAsync(id);
            return Ok(exercise);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateExercise([FromRoute] Guid id, string name, string description)
        {
            await exerciseService.UpdateAsync(id, name, description);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteExercise([FromRoute] Guid id)
        {
            await exerciseService.DeleteAsync(id);
            return NoContent();
        }
    }
}
