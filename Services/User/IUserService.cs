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

        if (user == null)
           return null;
        if (!VerifyPassword(password, user.PasswordHash))
            return null;

        return user;
    }
    private string HashPassword(string password)
    {
        var passwordHasher = new PasswordHasher<UserModel>();
        return passwordHasher.HashPassword(null, password);
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        var passwordHasher = new PasswordHasher<UserModel>();
        return passwordHasher
            .VerifyHashedPassword(null, storedHash, password) == PasswordVerificationResult.Success;
    }
}

