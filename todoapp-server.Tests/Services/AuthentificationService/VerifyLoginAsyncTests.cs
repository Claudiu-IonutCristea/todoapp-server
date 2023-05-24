using ErrorOr;
using Microsoft.EntityFrameworkCore;
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
public class VerifyLoginAsyncTests
{
	private readonly IAuthentificationService _authentificationService;
	private readonly DataContext _dbContext;

	public VerifyLoginAsyncTests(IAuthentificationService authentificationService, DataContext dbContext)
	{
		_authentificationService = authentificationService;
		_dbContext = dbContext;
	}

	[Theory]
	[MemberData(nameof(LoginFailed_Data))]
	[Trait("Authentification Service", "Verify Login Async")]
	public async Task LoginFailed(LoginDto loginDto, Error expectedError)
	{
		SetupDatabase();

		var result = await _authentificationService.VerifyLoginAsync(loginDto);

		Assert.True(result.IsError);
		Assert.NotEmpty(result.ErrorsOrEmptyList);
		Assert.Equal(expectedError, result.FirstError);
	}

	public static IEnumerable<object[]> LoginFailed_Data
		=> new List<object[]>
		{
			new object[] { new LoginDto { Email = "doesntexist@email.com", Password = "1234" }, Errors.Login.EmailNotFound },
			new object[] { new LoginDto { Email = "fakeuser@email.com", Password = "wrongpass" }, Errors.Login.PasswordIncorect },
		};


	[Theory]
	[MemberData(nameof(LoginSuccess_Data))]
	[Trait("Authentification Service", "Verify Login Async")]
	public async Task LoginSuccess(LoginDto loginDto)
	{
		SetupDatabase();

		var result = await _authentificationService.VerifyLoginAsync(loginDto);

		Assert.False(result.IsError);
		Assert.Empty(result.ErrorsOrEmptyList);
		Assert.Equal(Result.Success, result.Value);
	}

	public static IEnumerable<object[]> LoginSuccess_Data
		=> new List<object[]>
		{
			new object[] { new LoginDto { Email = "fakeuser@email.com", Password = "1234" } },
		};

	private void SetupDatabase()
	{
		_dbContext.Database.EnsureCreated();

		FakeDataGenerator.GetPasswordHashAndSalt("1234", out byte[] hash, out byte[] salt);
		var user = new User
		{
			Id = 1,
			Email = "fakeuser@email.com",
			Name = "John",
			PasswordHash = hash,
			PasswordSalt = salt
		};

		_dbContext.Users.Add(user);
		_dbContext.SaveChanges();
	}
}
