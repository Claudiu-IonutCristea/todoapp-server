using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using ToDoAppServer.Library.DTOs;
using ToDoAppServer.Library.Models;

namespace ToDoAppServer.API.Services;

public interface IRefreshTokenService
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

	Task<ErrorOr<RefreshToken>> ValidateRefreshTokenStringAsync(string token);

	ErrorOr<Device> ValidateRefreshTokenForUser(User user, RefreshToken token);

	Task RevokeTokenAsync(RefreshToken token);

	Task RevokeAllTokensAsync(List<RefreshToken> tokens);

	Task<int> ClearExpiredRevokedTokens();
}

public class RefreshTokenService : IRefreshTokenService
{
	private readonly DataContext _dbContext;

	public RefreshTokenService(DataContext dbContext)
	{
		_dbContext = dbContext;
	}

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

	public async Task<ErrorOr<RefreshToken>> ValidateRefreshTokenStringAsync(string token)
	{
		var dbToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token.Equals(token));

		if(dbToken == null)
			return Errors.RefreshToken.TokenInvalid;

		if(await _dbContext.RevokedRefreshTokens.ContainsAsync(dbToken))
		{
			_dbContext.RefreshTokens.Remove(dbToken);
			_dbContext.RevokedRefreshTokens.Remove(dbToken);
			await _dbContext.SaveChangesAsync();
			return Errors.RefreshToken.TokenRevoked;
		}	

		if(dbToken.Expires < DateTime.Now)
		{
			_dbContext.RefreshTokens.Remove(dbToken);
			await _dbContext.SaveChangesAsync();
			return Errors.RefreshToken.TokenExpired;
		}

		return dbToken;
	}

	public ErrorOr<Device> ValidateRefreshTokenForUser(User user, RefreshToken token)
	{
		var device = user.Devices.FirstOrDefault(x => x.RefreshToken.Equals(token));
		if(device == null)
			return Errors.RefreshToken.TokenNotValidForUser;

		return device;
	}

	public async Task RevokeTokenAsync(RefreshToken token)
	{
		await _dbContext.RevokedRefreshTokens.AddAsync(token);
		await _dbContext.SaveChangesAsync();
	}

	public async Task RevokeAllTokensAsync(List<RefreshToken> tokens)
	{
		foreach(var token in tokens)
		{
			await _dbContext.RevokedRefreshTokens.AddAsync(token);
		}

		await _dbContext.SaveChangesAsync();
	}

	public async Task<int> ClearExpiredRevokedTokens()
	{
		var expired = await _dbContext.RefreshTokens.Where(t => t.Expires < DateTime.Now).ToArrayAsync();
		_dbContext.RevokedRefreshTokens.RemoveRange(expired);

		await _dbContext.SaveChangesAsync();

		return expired.Length;
	}
}
