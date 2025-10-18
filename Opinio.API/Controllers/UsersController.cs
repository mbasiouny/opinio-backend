using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Opinio.API.Models.User;
using Opinio.Core.Entities;
using Opinio.Core.Helpers;
using Opinio.Core.Services;

namespace Opinio.API.Controllers;

[ApiController]
[Route("api/")]
public class UsersController(IUserService userService, IMapper mapper) : ControllerBase
{
    #region POST - Register
    [HttpPost("users/register")]
    public async Task<IActionResult> Register([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request);
        var createdUser = await userService.RegisterAsync(user, cancellationToken);

        var userResponse = mapper.Map<OperationResult<CreateUserResponse>>(createdUser);
        return Ok(userResponse);
    }
    #endregion

    #region Post - Login

    [HttpPost("users/login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request);
        var token = await userService.LoginAsync(user, cancellationToken);

        return Ok(token);
    }
    #endregion

    [Authorize]
    [HttpPost("users/logout")]
    public async Task<IActionResult> Logout(CancellationToken ct)
    {
        var result = await userService.LogoutAsync(User, ct);
        return Ok(result);
    }
}
