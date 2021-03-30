using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SGM.Legado.Api.STUR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGM.Legado.STUR.Api.Controllers
{
	[ApiController]
	[Route("/api/stur/health-check")]
	public class HealthCheckController : ControllerBase
	{
		[HttpGet]
		public IActionResult HealthCheck()
		{
			return Ok();
		}
	}
}
