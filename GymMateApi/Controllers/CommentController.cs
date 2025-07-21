using GymMateApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymMateApi.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController(ICommentService commentService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult> CreateComment(string text, Guid authorId, Guid trainingId)
        {
            var result = await commentService.CreateAsync(text, authorId, trainingId);
            return Ok(result);
        }

        [HttpGet("comments")]
        public async Task<ActionResult> GetAllComments()
        {
            var comments = await commentService.GetAllAsync();
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
