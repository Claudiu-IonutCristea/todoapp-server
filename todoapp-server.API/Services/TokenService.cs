using ToDoAppServer.Library.DTOs;

namespace ToDoAppServer.API.Services;

public partial interface ITokenService { }

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
