using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Core.Models;
using HomeMyDay.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace HomeMyDay.Web.Api.Controllers
{
	[Produces("application/json")]
	[Route("api/authentication")]
	public class AuthController : Controller
	{
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;

		public AuthController(UserManager<User> userMgr, SignInManager<User> signInMgr)
		{
			_signInManager = signInMgr;
			_userManager = userMgr;
		}

		// POST api/values
		[HttpPost("login")]
        public async Task<IActionResult> Post(string username, string password)
        {
			var user = await _userManager.FindByNameAsync(username);
			if(user == null)
			{
				return BadRequest();
			}

			if((await _signInManager.PasswordSignInAsync(username, password, false, false)).Succeeded)
			{
				return Ok();
			}

			return BadRequest();
		}

		// POST api/values
		[HttpPost("register")]
		public IActionResult Post(User user)
		{
			return null;
		}

		// POST api/values
		[HttpPost("forgotPassword")]
		public IActionResult Post(string email)
		{
			return null;
		}

		[HttpPut]
		public IActionResult Put()
		{
			return BadRequest();
		}

		[HttpDelete]
		public IActionResult Delete()
		{
			return BadRequest();
		}
    }
}
