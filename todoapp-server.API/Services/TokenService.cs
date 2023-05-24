using ToDoAppServer.Library.DTOs;

namespace ToDoAppServer.API.Services;

public interface ITokenService
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

public partial class TokenService : ITokenService
{
	private readonly IConfiguration _config;
	private readonly DataContext _dbContext;

	public TokenService(IConfiguration config, DataContext dbContext)
	{
		_config = config;
		_dbContext = dbContext;
	}
}
