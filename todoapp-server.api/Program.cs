global using ToDoAppServer.API.Data;
global using ToDoAppServer.Library.ServiceErrors;
global using ErrorOr;

using ToDoAppServer.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

//My extensions
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

app.UseExceptionHandler("/error");
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
