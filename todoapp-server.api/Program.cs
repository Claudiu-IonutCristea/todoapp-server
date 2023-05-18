global using ToDoAppServer.API.Data;
global using ToDoAppServer.Library.ServiceErrors;
global using ErrorOr;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using ToDoAppServer.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
	{
		Description = "Standard Authgiorization header using the Beared scheme (\"bearer {token}\")",
		In = ParameterLocation.Header,
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
	});

	options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
