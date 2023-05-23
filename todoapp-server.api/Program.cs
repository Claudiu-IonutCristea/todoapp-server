global using ToDoAppServer.API.Data;
global using ToDoAppServer.Library.ServiceErrors;
global using ErrorOr;

using ToDoAppServer.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
