using Microsoft.EntityFrameworkCore;
using ToDoAppServer.Library.Models;

namespace ToDoAppServer.API.Data;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options) : base(options) { }

	public DbSet<User> Users { get; set; }

	public DbSet<RefreshToken> RefreshTokens { get; set; }

	public DbSet<RefreshToken> RevokedRefreshTokens { get; set; }

	public DbSet<Device> UsersDevices { get; set; }
}
