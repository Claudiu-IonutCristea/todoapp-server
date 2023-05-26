using Microsoft.EntityFrameworkCore;
using ToDoAppServer.API.Services;

namespace ToDoAppServer.API.Extensions;

public static class ApplicationServiceExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection service, IConfiguration config)
	{
		service.AddScoped<IRefreshTokenService, RefreshTokenService>();
		service.AddScoped<IAccessTokenService, AccessTokenService>();
		service.AddScoped<IDeviceService, DeviceService>();

		service.AddScoped<IAuthentificationService, AuthentificationService>();

		service.AddDbContext<DataContext>(options =>
		{
			options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
		});

		return service;
	}
}
