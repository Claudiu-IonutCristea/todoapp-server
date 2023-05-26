using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoAppServer.API.Services;

namespace ToDoAppServer.API.Extensions;

public static class IdentityServiceExtensions
{
	public static IServiceCollection AddIdentityServices(this IServiceCollection service, IConfiguration config)
	{
		if(!AccessTokenService.GetAccessTokenSecurityKey(out SymmetricSecurityKey? securityKey, config))
			throw new Exception("Access token key is invalid!");

		service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = securityKey,
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero,
			};

			options.Events = new JwtBearerEvents
			{
				OnChallenge = context =>
				{
					context.HandleResponse();
					context.Response.StatusCode = StatusCodes.Status401Unauthorized;
					context.Response.ContentType = "application/json";

					Error error; //Declaring like this ensures that the error gets assigned for every case in the switch()
					switch(context.AuthenticateFailure)
					{
						case SecurityTokenExpiredException ex:
							error = Errors.Authorisation.TokenExpired;
							break;

						default:
							if(context.Error == null || context.ErrorDescription == null)
							{
								if(context.Request.Headers["Authorisation"].Equals(StringValues.Empty))
								{
									error = Errors.Authorisation.NoAuthHeader;
									break;
								}

								error = Errors.Authorisation.Unexpected;
								break;
							}	

							error = Error.Validation(code: context.Error, description: context.ErrorDescription);
							break;
					}

					context.Response.Headers.Add("Error-Code", error.Code);
					context.Error = error.Code;
					context.ErrorDescription = error.Description;

					return context.Response.WriteAsJsonAsync(new
					{
						error = context.Error,
						error_description = context.ErrorDescription
					});
				}
			};
		});

		return service;
	}
}
