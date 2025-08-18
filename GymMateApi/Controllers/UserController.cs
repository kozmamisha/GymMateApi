using GymMateApi.Application.Interfaces;
using GymMateApi.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace GymMateApi.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        await userService.Register(request.UserName, request.Email, request.Password, cancellationToken);

        return Created();
    }
}