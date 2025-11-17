using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Account;

namespace MVC.Controllers
{
    public class AccountController(UserManager<LocalIdentityUser> userManager, SignInManager<LocalIdentityUser> signInManager) : Controller
    {
        private readonly UserManager<LocalIdentityUser> _userManager = userManager;
        private readonly SignInManager<LocalIdentityUser> _signInManager = signInManager;

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = model.Email,
                    Password = model.Password,
                    IsAdmin = false
                };
                var identityUser = new LocalIdentityUser { UserName = model.Email, Email = model.Email, User = user };
                var result = await _userManager.CreateAsync(identityUser, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(identityUser, isPersistent: false);
                    // return RedirectToAction("Login", "Account");
                    return RedirectToAction(nameof(Login), nameof(AccountController).Replace("Controller", ""));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
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
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // return RedirectToAction("LoginSuccess", "Account");
                    return RedirectToAction(nameof(LoginSuccess), nameof(AccountController).Replace("Controller", ""));
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            // return RedirectToAction("Index", "Home");
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
        }

        public IActionResult LoginSuccess()
        {
            return View();
        }
    }
}
