using System.Security.Claims;
using GymMateApi.Application.Users.Commands.DeleteUser;
using GymMateApi.Application.Users.Commands.LoginUser;
using GymMateApi.Application.Users.Commands.RegisterUser;
using GymMateApi.Contracts.User;
using GymMateApi.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GymMateApi.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IMediator mediator, IOptions<AuthOptions> options) : ControllerBase
{
    private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(CustomClaims.UserId)!);
    
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        await mediator.Send(new RegisterUserCommand(request.UserName, request.Email, request.Password), cancellationToken);
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
    {
        var token = await mediator.Send(new LoginUserCommand(request.Email, request.Password), cancellationToken);
        return Ok(token);
    }

    [HttpPost("logout")]
    [Authorize]
    public ActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete(options.Value.CookieName);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<ActionResult> DeleteUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteUserCommand(id, CurrentUserId), cancellationToken);
        return NoContent();
    }
}