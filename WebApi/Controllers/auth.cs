using Application.Commands.UserCommands.RegisterUser;
using Application.Commands.UserCommands.LoginUser;
using Application.Commands.UserCommands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class Auth : ControllerBase
{
    private readonly IMediator _mediator;

    public Auth(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Registers a new user (Customer only).
    /// </summary>
    /// <param name="command">RegisterUserCommand containing user details.</param>
    /// <returns>Returns a UserWithTokenDto with user details and JWT token.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Register), result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    /// <summary>
    /// Logs in an existing user.
    /// </summary>
    /// <param name="command">LoginUserCommand containing email and password.</param>
    /// <returns>Returns a UserWithTokenDto with user details and JWT token.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
}