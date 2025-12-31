using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using MVC.Models.Account;

namespace MVC.Controllers;

public class AccountController(
    UserManager<LocalIdentityUser> userManager,
    SignInManager<LocalIdentityUser> signInManager,
    IConfiguration config) : Controller
{
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var existing = await userManager.FindByEmailAsync(model.Email);
        if (existing != null)
        {
            ModelState.AddModelError(nameof(model.Email), "A user with this email already exists.");
            return View(model);
        }

        var user = new User
        {
            Email = model.Email,
            Password = model.Password,
            IsAdmin = model.IsAdmin,
        };
        var identityUser = new LocalIdentityUser { UserName = model.Email, Email = model.Email, User = user };
        var result = await userManager.CreateAsync(identityUser, model.Password);

        if (result.Succeeded)
        {
            var addRolesResult =
                await userManager.AddToRolesAsync(identityUser, model.IsAdmin ? ["User", "Admin"] : ["User"]);
            if (!addRolesResult.Succeeded)
            {
                foreach (var error in addRolesResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            await signInManager.SignInAsync(identityUser, isPersistent: false);
            return RedirectToAction(nameof(Index), nameof(HomeController).Replace("Controller", ""));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            // return RedirectToAction("LoginSuccess", "Account");
            return RedirectToAction(nameof(LoginSuccess), nameof(AccountController).Replace("Controller", ""));
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        // return RedirectToAction("Index", "Home");
        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
    }

    public IActionResult LoginSuccess()
    {
        return View();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Token()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var roles = await userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
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

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return View(new TokenViewModel { Token = tokenString });
    }

    [HttpGet]
    public IActionResult ResetPassword() => View();

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            // Don't reveal that the user does not exist
            ModelState.AddModelError(string.Empty, "Internal error occured.");
            return View();
        }

        string decodedToken;
        try
        {
            var tokenBytes = WebEncoders.Base64UrlDecode(model.Token);
            decodedToken = Encoding.UTF8.GetString(tokenBytes);
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "Invalid token.");
            return View(model);
        }

        var result = await userManager.ResetPasswordAsync(user, decodedToken, model.Password);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Login), nameof(AccountController).Replace("Controller", ""));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }
}