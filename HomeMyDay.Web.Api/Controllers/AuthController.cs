using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Core.Models;
using HomeMyDay.Infrastructure.Identity;

namespace HomeMyDay.Web.Api.Controllers
{
	[Produces("application/json")]
	[Route("api/authentication")]
	public class AuthController : Controller
	{
		//private readonly IAuthManager AuthManager;

		//public AuthController(IAuthManager AuthMgr)
		//{
			//AuthManager = AuthMgr;
		//}

		[HttpGet]
		public IActionResult Get()
		{
			return BadRequest();
		}

		// POST api/values
		[HttpPost("login")]
        public IActionResult Post(string username, string password)
        {
			return null;
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
