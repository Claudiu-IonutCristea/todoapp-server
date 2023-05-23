using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoAppServer.Library.DTOs;

namespace ToDoAppServer.API.Controllers;

public class AuthController : ApiController
{
	[HttpPost("login")]
	public async Task<ActionResult<AuthTokentsDto>> Login(LoginDto login)
	{

		return Ok();
	}
}
