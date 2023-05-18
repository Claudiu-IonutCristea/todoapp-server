using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using ToDoAppServer.Library.DTOs;
using ToDoAppServer.Library.Models;

namespace ToDoAppServer.API.Services;

public interface IAuthentificationService
{
	public Task<ErrorOr<Success>> VerifyLogin(LoginDto login);
	public Task<ErrorOr<User>> RegisterUser(RegisterDto register);
}

public class AuthentificationService : IAuthentificationService
{
	private readonly DataContext _dbContext;

	public AuthentificationService(DataContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<ErrorOr<Success>> VerifyLogin(LoginDto login)
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

	public async Task<ErrorOr<User>> RegisterUser(RegisterDto register)
	{
		var dbUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == register.Email);

		if(dbUser != null)
			return Errors.Register.EmailAlreadyInUse;

		using var hmac = new HMACSHA512();
		var newUserHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password));

		return new User
		{
			Email = register.Email,
			Name = register.Name,
			PasswordSalt = hmac.Key,
			PasswordHash = newUserHash,
		};
	}
}


