using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prj.TaskManager.Data;
using Prj.TaskManager.Models;

namespace Prj.TaskManager.Controllers
{
    
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                var existingNumber = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber);
                if (existingUser != null)
                {
                    TempData["Message"] = "Email is Already Registered";
                    return View(model);
                }
                if (existingNumber != null)
                {
                    TempData["Message"] = "Number is Already Registered";
                    return View(model);
                }

                var user = new ApplicationUser
                {
                    FullName = model.FullName,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber

                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    TempData["Message"] = "Registered Successfully";
                    return RedirectToAction("Login","Auth");
                }
                ModelState.AddModelError("", "Registration failed. Please try again.");
            }
            TempData["Message"] = "Invalid";
            return View(model);
        }
        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {

            if(ModelState.IsValid)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(a => a.Email == model.Email);
                if(user==null|| !(await _userManager.CheckPasswordAsync(user,model.Password)))
                {
                    TempData["Message"] = "Invalid Credentials";
                    return View(model);
                }
                await  _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            TempData["Message"] = "invalid Credentials";
            return View(model);
        }
       
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

