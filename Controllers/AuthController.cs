using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;

    public AuthController(JwtService jwtService)
    {
        _jwtService = jwtService;
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