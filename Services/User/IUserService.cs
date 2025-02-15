using System.Security.Cryptography;
using System.Text;
using GamesAPI.Data;
using GamesAPI.DTOs.Authentication;
using GamesAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public interface IUserService
{
    Task<UserModel> RegisterUserAsync(RegisterUserDto request);
    Task<UserModel> AuthenticateAsync(string email, string password);
}

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    public UserService(AppDbContext context)
    {
        _context = context;

    }
    public async Task<UserModel> RegisterUserAsync(RegisterUserDto request)
    {
        var user = new UserModel
        {
            Email = request.Email,
            PasswordHash = HashPassword(request.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
    public async Task<UserModel> AuthenticateAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null && VerifyPassword(password, user.PasswordHash))
        {
            return user;
        }
        return null;
    }
    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        return HashPassword(password) == storedHash;
    }
}

