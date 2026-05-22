using System.Security.Claims;
using GymMateApi.Application.Comments.Comments.CreateComment;
using GymMateApi.Application.Comments.Comments.DeleteComment;
using GymMateApi.Application.Comments.Comments.UpdateComment;
using GymMateApi.Application.Comments.Queries.GetAllComments;
using GymMateApi.Application.Comments.Queries.GetCommentsByPage;
using GymMateApi.Contracts.Comment;
using GymMateApi.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymMateApi.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController(IMediator mediator) : ControllerBase
    {
        private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(CustomClaims.UserId)!);
        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateComment([FromBody] CreateCommentRequest request, CancellationToken cancellationToken)
        {
            await mediator.Send(new CreateCommentCommand(request.Text, request.TrainingId, CurrentUserId), cancellationToken);
            return Ok();
        }

        [HttpGet()]
        [Authorize]
        public async Task<ActionResult> GetAllComments(CancellationToken cancellationToken)
        {
            var comments = await mediator.Send(new GetAllCommentsQuery(), cancellationToken);
            return Ok(comments);
        }        
        
        [HttpGet("pagination")]
        [Authorize]
        public async Task<ActionResult> GetCommentsByPage([FromQuery] int page, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            var comments = await mediator.Send(new GetCommentsByPageQuery(page, pageSize), cancellationToken);
            return Ok(comments);
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<ActionResult> UpdateComment([FromRoute] Guid id, [FromBody] UpdateCommentRequest request, CancellationToken cancellationToken)
        {
            await mediator.Send(new UpdateCommentCommand(id, request.Text, CurrentUserId), cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<ActionResult> DeleteComment([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteCommentCommand(id, CurrentUserId), cancellationToken);
            return NoContent();
        }
    }
}
