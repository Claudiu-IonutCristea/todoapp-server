using System.Security.Cryptography;
using ToDoAppServer.Library.DTOs;

namespace ToDoAppServer.API.Services;

public partial class TokenService //Refresh Token
{
	public RefreshTokenDto CreateRefreshToken(TimeSpan lifeSpan)
	{
		var tokenString = Convert.ToBase64String(RandomNumberGenerator.GetBytes(66));

		var tokenDto = new RefreshTokenDto
		{
			Token = tokenString,
			Created = DateTime.Now,
			Expires = DateTime.Now + lifeSpan,
		};

		return tokenDto;
	}
}
