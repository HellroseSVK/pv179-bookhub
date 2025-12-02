using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BusinessLayer.DTOs.User;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Exception;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BusinessLayer.Services;

public class UserService(CustomDBContext dbContext, IConfiguration config) : IUserService
{
    public async Task<UserDTO> CreateUserAsync(UserUpsertDTO userDto)
    {
        if (await FindUserByEmail(userDto.Email) != null)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest,
                $"A user with email {userDto.Email} already exists.");
        }

        var created = dbContext.Users.Add(userDto.Adapt<User>());

        await dbContext.SaveChangesAsync();

        return created.Entity.Adapt<UserDTO>();
    }

    public async Task<string> GetTokenAsync(UserLoginDTO userDto)
    {
        var user = await FindUserByEmail(userDto.Email);
        if (user == null || user.Password != userDto.Password)
        {
            throw new StatusCodeException(StatusCodes.Status401Unauthorized, "Invalid email or password.");
        }

        string[] roles = user.IsAdmin ? ["Admin", "User"] : ["User"];

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var configuredKey = config["Jwt:Key"] ?? string.Empty;
        if (string.IsNullOrWhiteSpace(configuredKey))
        {
            throw new InvalidOperationException("JWT key is not configured. Set Jwt:Key in configuration.");
        }

        byte[] keyBytes;
        using (var sha = SHA256.Create())
        {
            keyBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(configuredKey));
        }

        var key = new SymmetricSecurityKey(keyBytes);
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<UserDTO> GetUserAsync(int id, bool includeReviews = true,
        bool includeWishlistItems = true, bool includeOrderItems = true)
    {
        var user = await FindUser(id, includeReviews, includeWishlistItems, includeOrderItems);

        return user.Adapt<UserDTO>();
    }

    public async Task<List<UserDTO>> GetUsersAsync(int[]? ids = null, bool includeReviews = true,
        bool includeWishlistItems = true, bool includeOrderItems = true)
    {
        var query = GetBaseQuery(includeReviews, includeWishlistItems, includeOrderItems);

        if (ids is { Length: > 0 })
        {
            query = query.Where(a => ids.Contains(a.Id));
        }

        var users = await query.ToListAsync();

        return users.Adapt<List<UserDTO>>();
    }

    public async Task PatchUserAsync(int id, UserPartialDTO userDto)
    {
        var loaded = await FindUser(id);

        if (userDto.Email != null)
        {
            await FindUserThrow(userDto.Email);
        }

        loaded.Email = userDto.Email ?? loaded.Email;
        loaded.Password = userDto.Password ?? loaded.Password;
        loaded.IsAdmin = userDto.IsAdmin ?? loaded.IsAdmin;

        dbContext.Users.Update(loaded);

        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(int id, UserUpsertDTO userDto)
    {
        var loaded = await FindUser(id);

        await FindUserThrow(userDto.Email);

        loaded.Email = userDto.Email;
        loaded.Password = userDto.Password;
        loaded.IsAdmin = userDto.IsAdmin;

        dbContext.Users.Update(loaded);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await FindUser(id);

        dbContext.Users.Remove(user);

        await dbContext.SaveChangesAsync();
    }

    private IQueryable<User> GetBaseQuery(bool includeReviews, bool includeWishlistItems, bool includeOrderItems)
    {
        IQueryable<User> userQuery = dbContext.Users;

        if (includeReviews)
        {
            userQuery = userQuery
                .Include(user => user.Reviews);
        }

        if (includeWishlistItems)
        {
            userQuery = userQuery
                .Include(user => user.WishlistItems);
        }

        if (includeOrderItems)
        {
            userQuery = userQuery
                .Include(user => user.OrderItems);
        }

        return userQuery;
    }

    private async Task<User> FindUser(int userId, bool includeReviews = false, bool includeWishlistItems = false,
        bool includeOrderItems = false)
    {
        var query = GetBaseQuery(includeReviews, includeWishlistItems, includeOrderItems);

        var user = await query.FirstOrDefaultAsync(user => user.Id == userId);

        return user ??
               throw new StatusCodeException(StatusCodes.Status404NotFound, $"User with id {userId} not found.");
    }

    private async Task<User?> FindUserByEmail(string email)
    {
        var user = await dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

        return user;
    }

    private async Task FindUserThrow(string email)
    {
        var user = await dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

        if (user != null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound, $"User with email {email} already exists.");
        }
    }
}