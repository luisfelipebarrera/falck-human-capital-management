using ApiSecurity.Models.Requests;
using ApiSecurity.Models.Responses;
using ApiSecurity.Security;
using ApiSecurity.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiSecurity.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    private readonly IJwtTokenGenerator _jwt;

    public AuthController(
        IUserService userService,
        IJwtTokenGenerator jwt)
    {
        _userService = userService;
        _jwt = jwt;
    }

    [HttpPost("login")]
    [ProducesResponseType<LoginResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<LoginResponse> Login(LoginRequest request)
    {
        var user = _userService.Authenticate(
            request.Username,
            request.Password);

        if (user is null)
        {
            return Unauthorized(new
            {
                message = "Invalid username or password."
            });
        }

        var token = _jwt.Generate(user);

        return Ok(new LoginResponse(token));
    }

    [HttpPost("users")]
    public ActionResult<UserResponse> Create(CreateUserRequest request)
    {
        var user = _userService.Create(request);

        return Created(
            $"/auth/users/{user.Id}",
            new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role
            });
    }

    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        return Ok(
            _userService.GetAll()
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    Username = u.Username,
                    Role = u.Role
                }));
    }
}