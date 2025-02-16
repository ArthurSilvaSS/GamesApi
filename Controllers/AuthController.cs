using GamesAPI.DTOs.Authentication;
using GamesAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly IUserService _userService;

    public AuthController(JwtService jwtService, IUserService userService)
    {
        _jwtService = jwtService;
        _userService = userService;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var user = await _userService.RegisterUserAsync(request);
            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Email);
            return Ok(new { Message = "User created successfully!", Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var user = await _userService.AuthenticateAsync(request);

        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }
        var token = _jwtService.GenerateToken(user.Id.ToString(), user.Email);
        return Ok(new { Token = token });
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }
}