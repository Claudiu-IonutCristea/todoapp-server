using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ToDoAppServer.API.Services;
using ToDoAppServer.Library.DTOs;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Features;

namespace ToDoAppServer.API.Controllers;

public class AuthController : ApiController
{
	private readonly IAccessTokenService _tokenService;

	public AuthController(IAccessTokenService tokenService)
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
		//var tokenErr = _tokenService.IsValidSignature(Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty));

		//return tokenErr.Match(
		//	token => Ok(JsonSerializer.Serialize(token.Claims.Select(claim => new {claimType = claim.Type, claimValue = claim.Value}))),
		//	errors => Problem(errors));

		var feature = Request.HttpContext.Features.Get<IHttpConnectionFeature>();
		var localIp = feature?.LocalIpAddress?.ToString();
		var remoteIp = feature?.RemoteIpAddress?.ToString();

		return Ok(JsonSerializer.Serialize(new { localIP = localIp, remoteIP = remoteIp })); ;

	}
}
