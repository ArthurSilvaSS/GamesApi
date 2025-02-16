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
    Task<UserModel> AuthenticateAsync(LoginRequestDto loginDTO);
    Task<IEnumerable<UserDto>> GetUsersAsync();
}

