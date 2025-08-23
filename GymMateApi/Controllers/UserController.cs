using GymMateApi.Application.Interfaces;
using GymMateApi.Contracts.User;
using GymMateApi.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GymMateApi.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IUserService userService, IOptions<AuthOptions> options) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        await userService.Register(request.UserName, request.Email, request.Password, cancellationToken);

        return Created();
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
    {
        var token = await userService.Login(request.Email, request.Password, cancellationToken);
        
        HttpContext.Response.Cookies.Append(options.Value.CookieName, token);

        return Created();
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
        await userService.DeleteAsync(id, cancellationToken);
        
        HttpContext.Response.Cookies.Delete(options.Value.CookieName);
        
        return NoContent();
    }
}