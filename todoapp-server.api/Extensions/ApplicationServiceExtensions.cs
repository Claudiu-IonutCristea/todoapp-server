using Microsoft.EntityFrameworkCore;

namespace ToDoAppServer.API.Extensions;

public static class ApplicationServiceExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection service, IConfiguration config)
	{
		//Dependency Injection
		//service.AddScoped<>

		service.AddDbContext<DataContext>(options =>
		{
			options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
		});

		return service;
	}
}
