using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoAppServer.Library.DTOs;

namespace ToDoAppServer.API.Services;

public interface IAccessTokenService
{
	/// <summary>
	///		Creates an access token (JWT) based on the user information <br/>
	/// </summary>
	/// 
	/// <param name="user">
	///		Required information: <br/>
	///		<see cref="UserDto.Id"/> (Required by model) <br/>
	///		<see cref="UserDto.Email"/> (Required by model)
	/// </param>
	/// 
	/// <param name="lifeSpan">
	///		Computes the expiration date of the token (Recomended: short lifespan)
	/// </param>
	/// 
	/// <returns>
	///		<see cref="ErrorOr"/>&lt;<see cref="string"/>&gt; with the possible values: <br/>
	///		<see cref="Errors.AccessToken.TokenKeyInvalid"/> (Unexpected error.
	///			Appears if the <see cref="IConfiguration"/> section for the access token secret key is not found) <br/>
	///		<see cref="string"/> containing the access token (JWT)
	/// </returns>
	ErrorOr<string> CreateAccessToken(UserDto user, TimeSpan lifeSpan);

	/// <summary>
	///		Verifies if the signature of a JWT token is valiid
	/// </summary>
	/// 
	/// <param name="token"></param>
	/// 
	/// <returns>
	///		<see cref="ErrorOr"/>&lt;<see cref="ClaimsPrincipal"/>&gt; with the possible values: <br/>
	///		<see cref="Errors.AccessToken.TokenSignatureInvalid"/> <br/>
	///		<see cref="ClaimsPrincipal"/> With the claims of the token
	/// </returns>
	ErrorOr<ClaimsPrincipal> IsValidSignature(string token);
}

public class AccessTokenService : IAccessTokenService
{
	private readonly IConfiguration _config;

	public AccessTokenService(IConfiguration config)
	{
		_config = config;
	}

	public static bool GetAccessTokenSecurityKey([NotNullWhen(true)] out SymmetricSecurityKey? securityKey, IConfiguration configuration)
	{
		var key = configuration.GetSection("TokenKeys:AccessToken").Value; //WHY IS THIS "MAYBE NULL" ???
		if(key == null)
		{
			securityKey = null;
			return false;
		}

		securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

		return true;
	}


	public ErrorOr<string> CreateAccessToken(UserDto user, TimeSpan lifeSpan)
	{
		var expiration = DateTime.Now + lifeSpan;

		var claims = new List<Claim>()
	{
		new(ClaimTypes.Expiration, expiration.ToString()),
		new(ClaimTypes.Email, user.Email),
		new("UserId", user.Id.ToString()),
	};

		if(!GetAccessTokenSecurityKey(out SymmetricSecurityKey? securityKey, _config))
			return Errors.AccessToken.TokenKeyInvalid;

		var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

		var token = new JwtSecurityToken(
				claims: claims,
				expires: expiration,
				signingCredentials: creds
			);

		var jwt = new JwtSecurityTokenHandler().WriteToken(token);

		return jwt;
	}

	public ErrorOr<ClaimsPrincipal> IsValidSignature(string token)
	{
		if(!GetAccessTokenSecurityKey(out SymmetricSecurityKey? securityKey, _config))
			return Errors.AccessToken.TokenKeyInvalid;

		var tokenHandler = new JwtSecurityTokenHandler();
		var validationParams = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = securityKey,
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = false,
		};

		try
		{
			return tokenHandler.ValidateToken(token, validationParams, out SecurityToken validatedToken);
		}
		catch(SecurityTokenInvalidSignatureException)
		{
			return Errors.AccessToken.TokenSignatureInvalid;
		}
	}
}
