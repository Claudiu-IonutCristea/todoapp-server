using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppServer.API.Data;
using ToDoAppServer.API.Services;
using ToDoAppServer.Library.DTOs;
using ToDoAppServer.Library.Models;
using ToDoAppServer.Library.ServiceErrors;

namespace ToDoAppServer.Tests.Services.AuthentificationService;
public class RegisterUserAsyncTests
{
	private readonly IAuthentificationService _authentificationService;
	private readonly DataContext _dbContext;

	public RegisterUserAsyncTests(IAuthentificationService authentificationService, DataContext dbContext)
	{
		_authentificationService = authentificationService;
		_dbContext = dbContext;
	}

	[Theory]
	[MemberData(nameof(RegisterFailed_Data))]
	[Trait("Authentification Service", "Register User Async")]
	public async Task RegisterFailed(RegisterDto registerDto, Error expectedError )
	{
		#region Database Setup
		_dbContext.Database.EnsureCreated();

		FakeDataGenerator.GetPasswordHashAndSalt("5555", out byte[] hash, out byte[] salt);
		var user = new User
		{
			Id = 1,
			Email = "inuse@email.com",
			PasswordHash = hash,
			PasswordSalt = salt,
		};

		_dbContext.Users.Add(user);
		_dbContext.SaveChanges();

		#endregion

		var result = await _authentificationService.RegisterUserAsync(registerDto);

		Assert.True(result.IsError);
		Assert.NotEmpty(result.ErrorsOrEmptyList);
		Assert.Equal(expectedError, result.FirstError);

		var users = _dbContext.Users.Where(u => u.Email.Equals(registerDto.Email));

		Assert.Single(users);
	}

	public static IEnumerable<object[]> RegisterFailed_Data
		=> new List<object[]>
		{
			new object[] { new RegisterDto { Email = "inuse@email.com", Name = "John", Password = "1234" }, Errors.Register.EmailAlreadyInUse },
		};


	[Theory]
	[MemberData(nameof(RegisterSuccess_Data))]
	[Trait("Authentification Service", "Register User Async")]
	public async Task RegisterSuccess(RegisterDto registerDto)
	{
		var expectedUser = new User
		{
			Id = 1,
			Email = registerDto.Email,
			Name = registerDto.Name,
		};

		var result = await _authentificationService.RegisterUserAsync(registerDto);

		Assert.False(result.IsError);
		Assert.Empty(result.ErrorsOrEmptyList);

		Assert.Multiple(
			() => Assert.Equal(expectedUser.Id, result.Value.Id),
			() => Assert.Equal(expectedUser.Email, result.Value.Email),
			() => Assert.Equal(expectedUser.Name, result.Value.Name)
		);

		var users = _dbContext.Users.Where(u => u.Email.Equals(registerDto.Email)).ToList();
		Assert.Single(users);
		Assert.Equivalent(result.Value, users[0]);
	}

	public static IEnumerable<object[]> RegisterSuccess_Data
		=> new List<object[]>
		{
			new object[] { new RegisterDto { Email = "john@email.com", Name = "John", Password = "1234" } },
		};
}
