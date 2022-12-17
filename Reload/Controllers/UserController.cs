using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reload.Models.DataTables;
using System.Security.Claims;

namespace Reload.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var userToPopulate = new IdentityUser()
                {
                    UserName = user.Username,
                    Email = user.Email,
                };

                var result = await userManager.CreateAsync(userToPopulate, user.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(userToPopulate, false);
                    return RedirectToAction("Start", "Home");
                }
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(user.Username, user.Password, user.RememberMe, false);
                if (identityResult.Succeeded)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    Console.WriteLine(userId);
                    return RedirectToAction("DisplayCourses", "Home");
                }
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Start", "Home");
        }
    }
}
