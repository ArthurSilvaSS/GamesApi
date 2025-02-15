using GamesAPI.DTOs.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

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
            // Opcionalmente, gere um token logo após o registro
            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Email);
            return Ok(new { Message = "Usuário criado com sucesso!", Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Email == "admin@admin.com" && request.Password == "admin")
        {
            var token = _jwtService.GenerateToken("123", request.Email);
            return Ok(new {Token = token});
        }
        return Unauthorized();
    }
}