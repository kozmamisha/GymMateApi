using GymMateApi.Application.Interfaces;
using GymMateApi.Contracts.Exercise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymMateApi.Controllers
{
    [ApiController]
    [Route("api/exercises")]
    public class ExerciseController(IExerciseService exerciseService) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateExercise([FromBody] CreateExerciseRequest request, CancellationToken cancellationToken)
        {
            await exerciseService.CreateAsync(request.Name, request.Description, request.TrainingId, cancellationToken);
            return Ok();
        }

        [HttpGet()]
        [Authorize]
        public async Task<ActionResult> GetAllExercises(CancellationToken cancellationToken)
        {
            var exercises = await exerciseService.GetAllAsync(cancellationToken);
            return Ok(exercises);
        }
        
        [HttpGet("pagination")]
        [Authorize]
        public async Task<ActionResult> GetExercisesByPage([FromQuery] int page, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            var exercises = await exerciseService.GetByPage(page, pageSize, cancellationToken);
            return Ok(exercises);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult> GetOneExercise([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var exercise = await exerciseService.GetByIdAsync(id, cancellationToken);
            return Ok(exercise);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateExercise([FromRoute] Guid id, [FromBody] UpdateExerciseRequest request, CancellationToken cancellationToken)
        {
            await exerciseService.UpdateAsync(id, request.Name, request.Description, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteExercise([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await exerciseService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
