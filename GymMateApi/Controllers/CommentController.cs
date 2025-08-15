using GymMateApi.Application.Interfaces;
using GymMateApi.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GymMateApi.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController(ICommentService commentService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateComment([FromBody] CreateCommentRequest request, CancellationToken cancellationToken)
        {
            await commentService.CreateAsync(request.Text, request.AuthorId, request.TrainingId, cancellationToken);
            return Ok();
        }

        [HttpGet()]
        public async Task<ActionResult> GetAllComments(CancellationToken cancellationToken)
        {
            var comments = await commentService.GetAllAsync(cancellationToken);
            return Ok(comments);
        }        
        
        [HttpGet("pagination")]
        public async Task<ActionResult> GetCommentsByPage([FromQuery] int page, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            var comments = await commentService.GetByPageAsync(page, pageSize, cancellationToken);
            return Ok(comments);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateComment([FromRoute] Guid id, [FromBody] UpdateCommentRequest request, CancellationToken cancellationToken)
        {
            await commentService.UpdateAsync(id, request.Text, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteComment([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await commentService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
