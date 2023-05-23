using Microsoft.EntityFrameworkCore;
using ToDoAppServer.API.Services;

namespace ToDoAppServer.API.Extensions;

public static class ApplicationServiceExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection service, IConfiguration config)
	{
		service.AddScoped<ITokenService, TokenService>();
		service.AddScoped<IAuthentificationService, AuthentificationService>();

		service.AddDbContext<DataContext>(options =>
		{
			options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
		});

		return service;
	}
}
