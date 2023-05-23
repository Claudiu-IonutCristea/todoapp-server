using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using ToDoAppServer.Library.Models;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace ToDoAppServer.API.Services;

public interface ITokenService
{
	string CreateAccessToken(User user);
}

public class TokenService : ITokenService
{
	private readonly IConfiguration _config;

	public TokenService(IConfiguration config)
	{
		_config = config;
	}

	public string CreateAccessToken(User user)
	{
		var claims = new List<Claim>()
		{
			new(ClaimTypes.Email, user.Email)
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
				_config.GetSection("TokenKeys:AccessToken").Value
			));

		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

		var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: creds
			);

		var jwt = new JwtSecurityTokenHandler().WriteToken(token);

		return jwt;
	}
}
