using GymMateApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymMateApi.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController(ICommentService commentService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult> CreateComment(string text, Guid authorId, Guid trainingId)
        {
            await commentService.CreateAsync(text, authorId, trainingId);
            return Ok();
        }

        [HttpGet()]
        public async Task<ActionResult> GetAllComments()
        {
            var comments = await commentService.GetAllAsync();
            return Ok(comments);
        }        
        
        [HttpGet("pagination")]
        public async Task<ActionResult> GetCommentsByPage([FromQuery] int page, [FromQuery] int pageSize)
        {
            var comments = await commentService.GetByPageAsync(page, pageSize);
            return Ok(comments);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateComment([FromRoute] Guid id, [FromBody] string text)
        {
            await commentService.UpdateAsync(id, text);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteComment([FromRoute] Guid id)
        {
            await commentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
