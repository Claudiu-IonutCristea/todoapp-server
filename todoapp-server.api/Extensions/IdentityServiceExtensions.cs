using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ToDoAppServer.API.Extensions;

public static class IdentityServiceExtensions
{
	public static IServiceCollection AddIdentityServices(this IServiceCollection service, IConfiguration config)
	{
		service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["AccessTokenKey"])),
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero,
			};
		});

		return service;
	}
}
