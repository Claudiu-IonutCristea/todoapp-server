using System.Security.Cryptography;
using ToDoAppServer.Library.DTOs;

namespace ToDoAppServer.API.Services;

public partial interface ITokenService
{
	/// <summary>
	///		Creates a <see langword="new"/> <see cref="RefreshToken"/>
	/// </summary>
	/// 
	/// <param name="lifeSpan">
	///		Computes the expiration date of the token (Recomended: long lifespan)
	/// </param>
	/// 
	/// <returns>
	///		<see langword="new"/> <see cref="RefreshTokenDto"/> with infromation for <br/>
	///		<see cref="RefreshTokenDto.Token"/> <br/>
	///		<see cref="RefreshTokenDto.Created"/> <br/>
	///		<see cref="RefreshTokenDto.Expires"/>
	/// </returns>
	RefreshTokenDto CreateRefreshToken(TimeSpan lifeSpan);
}

public partial class TokenService : ITokenService //Refresh Token
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
