using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using WebApplication7.Models;
using WebApplication7.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace WebApplication7.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment webHostEnvironment, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel Userrvm)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(Userrvm.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email is already taken.");
                    return View(Userrvm);
                }

                ApplicationUser userModel = new ApplicationUser
                {
                    UserName = Userrvm.UserName,
                    Email = Userrvm.Email
                };

                IdentityResult result = await _userManager.CreateAsync(userModel, Userrvm.Password);

                if (result.Succeeded)
                {
                    // Ensure roles exist before assigning
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    }

                    if (!await _roleManager.RoleExistsAsync("Visitor"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Visitor"));
                    }

                    if (Userrvm.Email == "admiin@gmail.com")
                    {
                        await _userManager.AddToRoleAsync(userModel, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(userModel, "Visitor");
                    }

                    await _signInManager.SignInAsync(userModel, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(Userrvm);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(loginViewModel.Email);

                if (user != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                    if (found)
                    {
                        await _signInManager.SignInAsync(user, loginViewModel.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Your password is incorrect.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "User does not exist.");
                }
            }
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
