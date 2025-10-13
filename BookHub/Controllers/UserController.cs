using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Core;
using WebAPI.Exception;
using WebAPI.Models;
using WebAPI.Models.Response;
using WebAPI.Models.Response.Simplified;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly CustomDBContext _dbContext;

    public UserController(CustomDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    [HttpGet]
    [ProducesResponseType<List<UserResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _dbContext.Users
            .Include(user => user.Reviews)
            .Include(user => user.WishlistItems)
            .Include(user => user.OrderItems)
            .ToListAsync();

        return Ok(users.Select(u => u.Adapt<UserResponse>() with
        {
            Reviews = u.Reviews.Select(r => r.Adapt<ReviewSimplifiedResponse>()).ToList(),
            WishlistItems = u.WishlistItems.Select(wi => wi.Adapt<WishlistItemSimplifiedResponse>()).ToList(),
            OrderItems = u.OrderItems.Select(oi => oi.Adapt<OrderItemSimplifiedResponse>()).ToList()
        }).ToList());
    }

    [HttpGet("{id}")]
    [ProducesResponseType<UserResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await (FindUser(id));

        return Ok(user.Adapt<UserResponse>() with
        {
            Reviews = user.Reviews.Select(r => r.Adapt<ReviewSimplifiedResponse>()).ToList(),
            WishlistItems = user.WishlistItems.Select(wi => wi.Adapt<WishlistItemSimplifiedResponse>()).ToList(),
            OrderItems = user.OrderItems.Select(oi => oi.Adapt<OrderItemSimplifiedResponse>()).ToList()
        });
    }

    [HttpPost]
    [ProducesResponseType<UserResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] UserModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var user = new User
        {
            Email = model.Email,
            Password = model.Password,
            IsAdmin = model.IsAdmin
        };

        _dbContext.Users.Add(user);

        await _dbContext.SaveChangesAsync();

        return Created("/User/{user.Id}", user.Adapt<UserResponse>() with
        {
            Reviews = user.Reviews.Select(r => r.Adapt<ReviewSimplifiedResponse>()).ToList(),
            WishlistItems = user.WishlistItems.Select(wi => wi.Adapt<WishlistItemSimplifiedResponse>()).ToList(),
            OrderItems = user.OrderItems.Select(oi => oi.Adapt<OrderItemSimplifiedResponse>()).ToList()
        });
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchUser(int id, [FromBody] UserPartialModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var user = await FindUser(id);

        user.Email = model.Email ?? user.Email;
        user.Password = model.Password ?? user.Password;
        user.IsAdmin = model.IsAdmin ?? user.IsAdmin;

        _dbContext.Users.Update(user);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var user = await FindUser(id);

        user.Email = model.Email;
        user.Password = model.Password;
        user.IsAdmin = model.IsAdmin;

        _dbContext.Users.Update(user);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var user = await FindUser(id);

        _dbContext.Users.Remove(user);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    private async Task<User> FindUser(int userId)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(p => p.Id == userId);

        if (user == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"User with id {userId} not found.");
        }

        return user;
    }
}