using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using ToDoAppServer.Library.DTOs;
using ToDoAppServer.Library.Models;

namespace ToDoAppServer.API.Services;

public interface IAuthentificationService
{
	/// <summary>
	///		Verifies login credentials from <paramref name="login"/>
	/// </summary>
	/// 
	/// <param name="login">Login data</param>
	/// 
	/// <returns>
	///		<see cref="ErrorOr"/>&lt;<see cref="Success"/>&gt; with the possible values: <br/>
	///		<see cref="Errors.Login.EmailNotFound"/> <br/>
	///		<see cref="Errors.Login.PasswordIncorect"/> <br/>
	///		<see cref="Result.Success"/>
	/// </returns>
	public Task<ErrorOr<Success>> VerifyLoginAsync(LoginDto login);

	/// <summary>
	///		Checks if a user with the specified email exists. If it doesn't,
	///		creates a new user based on the <see cref="RegisterDTO"/> <paramref name="register"/>
	///		and adds the user to the <see cref="DataContext"/>
	/// </summary>
	/// 
	/// <param name="register">Register data</param>
	/// 
	/// <returns>
	///		<see cref="ErrorOr"/>&lt;<see cref="User"/>&gt; with the possible values: <br/>
	///		<see cref="Errors.Register.EmailAlreadyInUse"/> <br/>
	///		<see langword="new"/> <see cref="User"/> that has just been added to the database.
	/// </returns>
	public Task<ErrorOr<User>> RegisterUserAsync(RegisterDto register);
}

public class AuthentificationService : IAuthentificationService
{
	private readonly DataContext _dbContext;

	public AuthentificationService(DataContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<ErrorOr<Success>> VerifyLoginAsync(LoginDto login)
	{
		var dbUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == login.Email);

		if(dbUser == null)
			return Errors.Login.EmailNotFound;

		using var hmac = new HMACSHA512(dbUser.PasswordSalt);
		var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

		var isCorrectPassword = dbUser.PasswordHash.SequenceEqual(computedHash);

		if(!isCorrectPassword)
			return Errors.Login.PasswordIncorect;

		return Result.Success;
	}

	public async Task<ErrorOr<User>> RegisterUserAsync(RegisterDto register)
	{
		var dbUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == register.Email);

		if(dbUser != null)
			return Errors.Register.EmailAlreadyInUse;

		using var hmac = new HMACSHA512();
		var newUserHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password));

		var user = new User
		{
			Email = register.Email,
			Name = register.Name,
			PasswordSalt = hmac.Key,
			PasswordHash = newUserHash,
		};

		await _dbContext.Users.AddAsync(user);
		await _dbContext.SaveChangesAsync();

		return user;
	}
}


