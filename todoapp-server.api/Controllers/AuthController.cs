using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ToDoAppServer.API.Services;
using ToDoAppServer.Library.DTOs;
using System.Text.Json;

namespace ToDoAppServer.API.Controllers;

public class AuthController : ApiController
{
	private readonly ITokenService _tokenService;

	public AuthController(ITokenService tokenService)
	{
		_tokenService = tokenService;
	}

	[HttpGet("token")]
	public ActionResult<string> GetToken()
	{
		var user = new UserDto
		{
			Id = 0,
			Email = "test@email.com",
		};

		var tokenErr = _tokenService.CreateAccessToken(user, TimeSpan.FromMinutes(1));

		return tokenErr.Value;
	}

	[HttpGet("test")]
	[Authorize]
	public ActionResult TestToken()
	{
		

		return Ok();
	}

	[HttpGet("expired")]
	public IActionResult ValidateToken()
	{
		var tokenErr = _tokenService.IsValidSignature(Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty));

		return tokenErr.Match(
			token => Ok(JsonSerializer.Serialize(token.Claims.Select(claim => new {claimType = claim.Type, claimValue = claim.Value}))),
			errors => Problem(errors));


	}
}
