using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoAppServer.Library.DTOs;

namespace ToDoAppServer.API.Services;

public partial class TokenService //Access Token (JWT)
{
	public ErrorOr<string> CreateAccessToken(UserDto user, TimeSpan lifeSpan)
	{
		var expiration = DateTime.Now + lifeSpan;

		var claims = new List<Claim>()
		{
			new(ClaimTypes.Expiration, expiration.ToString()),
			new(ClaimTypes.Email, user.Email),
			new("UserId", user.Id.ToString()),
		};

		var key = _config.GetSection("TokenKeys:AccessToken").Value;
		if(key == null)
			return Errors.AccessToken.TokenKeyInvalid;

		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

		var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

		var token = new JwtSecurityToken(
				claims: claims,
				expires: expiration,
				signingCredentials: creds
			);

		var jwt = new JwtSecurityTokenHandler().WriteToken(token);

		return jwt;
	}

}
