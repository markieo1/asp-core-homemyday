using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMyDay.Web.Api.Controllers
{
	[Produces("application/json")]
	[Route("api/v1/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
