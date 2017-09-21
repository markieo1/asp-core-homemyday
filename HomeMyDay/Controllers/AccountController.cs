using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using HomeMyDay.ViewModels;
using HomeMyDay.Repository;
using HomeMyDay.Identity;

namespace HomeMyDay.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private IUserRepository _IUserRepository;

        public AccountController(IUserRepository repo, UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
        {
            _IUserRepository = repo;
            _signInManager = signInMgr;
            _userManager = userMgr;
        }

        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
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
                    return Redirect(loginModel?.ReturnUrl ?? "/home");
                }
            }
            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        [AllowAnonymous]
        public ViewResult Register(string returnUrl)
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
                var user = new IdentityUser { UserName = registerModel.Username, Email = registerModel.Email };
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            return View();
        }
    }
}