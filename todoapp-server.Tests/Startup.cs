using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ToDoAppServer.API.Data;
using ToDoAppServer.API.Services;

namespace ToDoAppServer.Tests;

//https://github.com/pengweiqhca/Xunit.DependencyInjection

public class Startup
{
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddDbContext<DataContext>(options =>
		{
			options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
		});

		services.AddTransient<IAccessTokenService, AccessTokenService>();
		services.AddTransient<IRefreshTokenService, RefreshTokenService>();
		services.AddTransient<IDeviceService, DeviceService>();
	}
}
