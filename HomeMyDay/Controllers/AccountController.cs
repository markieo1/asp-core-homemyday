using System.Threading.Tasks;
using HomeMyDay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using HomeMyDay.ViewModels;
using HomeMyDay.Extensions;

namespace HomeMyDay.Controllers
{
	[Authorize]
    public class AccountController : Controller
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;

        public AccountController(UserManager<User> userMgr, SignInManager<User> signInMgr)
        {
            _signInManager = signInMgr;
            _userManager = userMgr;
        }

        [AllowAnonymous]
        public ViewResult Login(string returnUrl = "/")
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                if ((await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, false, false)).Succeeded)
                {
                    return Redirect(loginModel?.ReturnUrl ?? StringExtensions.TrimControllerName(Url.Action(nameof(HomeController))));
                }
            }
            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }

        public async Task<ViewResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return View();
        }

        [AllowAnonymous]
        public ViewResult Register(string returnUrl = "/")
        {
            return View(new RegisterViewModel
            {
                ReturnUrl = returnUrl
            });
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = registerModel.Username, Email = registerModel.Email };
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(HomeController.Index));
                }
            }
            return View();
        }
    }
}