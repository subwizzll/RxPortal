using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RxAPI.Interfaces.Services;
using RxAPI.Models.DTO;

namespace RxAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    readonly ILogger<LoginController> _logger;
    readonly IUserService _userService;
    readonly ILoginService _loginService;

    public LoginController(ILogger<LoginController> logger, IUserService userService, ILoginService loginService)
    {
        _logger = logger;
        _userService = userService;
        _loginService = loginService;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Login(UserDTO userDto)
    {
        if (string.IsNullOrWhiteSpace(userDto.Email) || string.IsNullOrWhiteSpace(userDto.Password))
        {
            _logger.LogError("Login: BadRequest");
            return BadRequest();
        }

        var verifiedUser = _userService.GetUser(userDto);
        if (verifiedUser == null)
            return NotFound("Login: Not Found");
        
        var token = _loginService.Login(verifiedUser);
        if (string.IsNullOrWhiteSpace(token))
        {
            _logger.LogError("Login: Unauthorized {UserDtoEmail}", userDto.Email);
            return Unauthorized();
        }

        _logger.LogInformation("Login: OK");
        return Ok(token);
    }
}