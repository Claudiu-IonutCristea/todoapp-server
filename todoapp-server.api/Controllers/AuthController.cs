using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todoapp_server.Library.DTOs;

namespace todoapp_server.API.Controllers;

public class AuthController : ApiController
{
	[HttpPost("login")]
	public async Task<ActionResult<AuthTokentsDto>> Login(LoginDto login)
	{


		return Ok();
	}
}
