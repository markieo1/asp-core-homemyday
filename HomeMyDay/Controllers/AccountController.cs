using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using HomeMyDay.ViewModels;
using HomeMyDay.Repository;
using HomeMyDay.Identity;
using HomeMyDay.Services;

namespace HomeMyDay.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private IUserRepository _IUserRepository;
        private IEmailServices _emailServices;

        public AccountController(IUserRepository repo,IEmailServices emailServices , UserManager<User> userMgr, SignInManager<User> signInMgr)
        {
            _IUserRepository = repo;
            _signInManager = signInMgr;
            _userManager = userMgr;
            _emailServices = emailServices;
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

                // Require the user to have a confirmed email before they can log on.
                var user = await _userManager.FindByNameAsync(loginModel.Username);
                if (user != null)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty,
                                      "You must have a confirmed email to log in.");
                        return View();
                    }
                }

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
                var user = new User { UserName = registerModel.Username, Email = registerModel.Email };
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    //Generate EmailConfirmationToken
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account",
                        new { userid = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);

                    await _emailServices.SendEmailAsync(user.Email, "Confirm your account",
                        "Please confirm your account by clicking this link:"  + callbackUrl);

                    //This log the user automatic in
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            return View();
        }

        // GET: /Account/ConfirmEmail
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
    }
}