using ApiSecurity.Models.Requests;
using Contracts.Responses;
using ApiSecurity.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiSecurity.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public ActionResult<UserResponse> Create(CreateUserRequest request)
    {
        var user = _userService.Create(request);

        return CreatedAtAction(
            nameof(GetUser),
            new { id = user.Id },
            new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role
            });
    }

    [HttpGet]
    [ProducesResponseType<PagedResponse<UserResponse>>(StatusCodes.Status200OK)]
    public ActionResult<PagedResponse<UserResponse>> GetUsers(int page = 1, int limit = 10)
    {
        var users = _userService.GetAll();

        var items = users
            .Skip((page - 1) * limit)
            .Take(limit)
            .Select(u => new UserResponse
            {
                Id = u.Id,
                Username = u.Username,
                Role = u.Role
            })
            .ToList();

        return Ok(new PagedResponse<UserResponse>
        {
            Items = items,
            Page = page,
            Limit = limit,
            Total = users.Count
        });
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<UserResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<UserResponse> GetUser(Guid id)
    {
        var user = _userService.GetById(id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(new UserResponse
        {
            Id = user.Id,
            Username = user.Username,
            Role = user.Role
        });
    }
}