using BusinessLayer.DTOs.User;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Shared.Exception;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class UserServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder =
        new MockedDependencyInjectionBuilder().AddServices().AddMockedDBContext();

    [Fact]
    public async Task CreateUser_ShouldReturnUserDto()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var userService = provider.GetRequiredService<IUserService>();

        var userDto = new UserUpsertDTO
        {
            Email = "newuser@example.com",
            Password = "newPassword123",
            IsAdmin = false
        };

        // Act
        var created = await userService.CreateUserAsync(userDto);

        // Assert
        Assert.NotNull(created);
        Assert.True(created.Id > 0);
        Assert.Equal(userDto.Email, created.Email);
        Assert.Equal(userDto.Password, created.Password);
        Assert.Equal(userDto.IsAdmin, created.IsAdmin);
        Assert.NotNull(created.Reviews);
        Assert.NotNull(created.WishlistItems);
        Assert.NotNull(created.Orders);
    }

    [Fact]
    public async Task GetUser_ShouldReturnUserDto()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var userService = provider.GetRequiredService<IUserService>();

        const int userId = 1;

        // Act
        var user = await userService.GetUserAsync(userId);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(userId, user.Id);
        Assert.False(string.IsNullOrEmpty(user.Email));
        Assert.NotNull(user.Reviews);
        Assert.NotNull(user.WishlistItems);
        Assert.NotNull(user.Orders);
    }

    [Fact]
    public async Task GetUser_ShouldThrowException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var userService = provider.GetRequiredService<IUserService>();

        const int invalidUserId = 9999;

        // Act + Assert
        await Assert.ThrowsAsync<StatusCodeException>(() =>
            userService.GetUserAsync(invalidUserId));
    }

    [Fact]
    public async Task GetUsers_ShouldReturnListOfUserDto()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var userService = provider.GetRequiredService<IUserService>();

        // Act
        var users = await userService.GetUsersAsync();

        // Assert
        Assert.NotNull(users);
        Assert.True(users.Count > 0);
        Assert.All(users, u =>
        {
            Assert.True(u.Id > 0);
            Assert.False(string.IsNullOrEmpty(u.Email));
        });
    }

    [Fact]
    public async Task GetUsers_WithIds_ShouldReturnFilteredUsers()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var userService = provider.GetRequiredService<IUserService>();

        int[] targetIds = [1, 2];

        // Act
        var users = await userService.GetUsersAsync(ids: targetIds);

        // Assert
        Assert.NotNull(users);
        Assert.Equal(2, users.Count);
        Assert.All(users, u => Assert.Contains(u.Id, targetIds));
    }

    [Fact]
    public async Task PatchUser_ShouldUpdateUser()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var userService = provider.GetRequiredService<IUserService>();

        var patchDto = new UserPartialDTO
        {
            Email = "updated@example.com"
        };

        const int userId = 1;

        // Act
        await userService.PatchUserAsync(userId, patchDto);
        var updated = await userService.GetUserAsync(userId);

        // Assert
        Assert.Equal("updated@example.com", updated.Email);
    }

    [Fact]
    public async Task UpdateUser_ShouldUpdateUser()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var userService = provider.GetRequiredService<IUserService>();

        var updateDto = new UserUpsertDTO
        {
            Email = "fullyupdated@example.com",
            Password = "newSecurePassword456",
            IsAdmin = true
        };

        const int userId = 1;

        // Act
        await userService.UpdateUserAsync(userId, updateDto);
        var updated = await userService.GetUserAsync(userId);

        // Assert
        Assert.Equal("fullyupdated@example.com", updated.Email);
        Assert.Equal("newSecurePassword456", updated.Password);
        Assert.True(updated.IsAdmin);
    }

    [Fact]
    public async Task DeleteUser_ShouldDeleteUser()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var userService = provider.GetRequiredService<IUserService>();

        const int userId = 1;

        // Act
        await userService.DeleteUserAsync(userId);

        // Assert
        await Assert.ThrowsAsync<StatusCodeException>(() =>
            userService.GetUserAsync(userId));
    }

    [Fact]
    public async Task DeleteUser_ShouldThrowException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var userService = provider.GetRequiredService<IUserService>();

        const int invalidUserId = 999999;

        // Act + Assert
        await Assert.ThrowsAsync<StatusCodeException>(() =>
            userService.DeleteUserAsync(invalidUserId));
    }


    [Fact]
    public async Task PatchUser_WithNullValues_ShouldPreserveExistingData()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var userService = provider.GetRequiredService<IUserService>();

        const int userId = 1;
        var originalUser = await userService.GetUserAsync(userId);

        var patchDto = new UserPartialDTO
        {
            Email = null,
            Password = "newPassword",
            IsAdmin = null
        };

        // Act
        await userService.PatchUserAsync(userId, patchDto);
        var updated = await userService.GetUserAsync(userId);

        // Assert
        Assert.Equal(originalUser.Email, updated.Email);
        Assert.Equal("newPassword", updated.Password);
        Assert.Equal(originalUser.IsAdmin, updated.IsAdmin);
    }

    [Fact]
    public async Task PatchUser_ShouldThrowException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var userService = provider.GetRequiredService<IUserService>();

        var patchDto = new UserPartialDTO
        {
            Email = "invalid@example.com"
        };

        const int invalidUserId = 9999;

        // Act + Assert
        await Assert.ThrowsAsync<StatusCodeException>(() =>
            userService.PatchUserAsync(invalidUserId, patchDto));
    }

    [Fact]
    public async Task UpdateUser_ShouldThrowException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var userService = provider.GetRequiredService<IUserService>();

        var updateDto = new UserUpsertDTO
        {
            Email = "test@example.com",
            Password = "testPassword",
            IsAdmin = false
        };

        const int invalidUserId = 9999;

        // Act + Assert
        await Assert.ThrowsAsync<StatusCodeException>(() =>
            userService.UpdateUserAsync(invalidUserId, updateDto));
    }

    [Fact]
    public async Task CreateUser_WithAdminFlag_ShouldCreateAdminUser()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var userService = provider.GetRequiredService<IUserService>();

        var userDto = new UserUpsertDTO
        {
            Email = "admin@newtest.com",
            Password = "adminPassword",
            IsAdmin = true
        };

        // Act
        var created = await userService.CreateUserAsync(userDto);

        // Assert
        Assert.NotNull(created);
        Assert.True(created.IsAdmin);
        Assert.Equal("admin@newtest.com", created.Email);
    }

}