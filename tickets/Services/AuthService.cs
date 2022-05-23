using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using tickets.Dto;
using tickets.Models.Entities;


namespace tickets.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly TicketsContext _context;

    public AuthService(IConfiguration configuration, TicketsContext context)
    {
        _configuration = configuration;
        _context = context;
    }
    
    public async Task<string?> Register(UserDto userDto)
    {
        CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        Console.WriteLine(Encoding.Default.GetString(passwordHash));
        User user = new User
        {
            Email = userDto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Name = userDto.Name,
            LastName = userDto.LastName,
            PhoneNumber = userDto.PhoneNumber,
            Type = "User"
        };
        _context.Users.Add(user);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return null;
        }
        return CreateToken(user.Id);
    }

    public async Task<string?> Login(LoginDto loginDto)
    {
        var currentUser = await (from user in _context.Users
            where user.Email == loginDto.Email
            select new {user.Id,user.PasswordHash, user.PasswordSalt, user.Type}).FirstAsync();
        if (VerifyPasswordHash(loginDto.Password, currentUser.PasswordHash,
                currentUser.PasswordSalt))
        {
            return CreateToken(currentUser.Id,currentUser.Type);
        }
        return null;
    }
    
    private string CreateToken(int userId, string type="User")
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, type)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(365),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}