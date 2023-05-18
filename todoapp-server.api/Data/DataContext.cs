using Microsoft.EntityFrameworkCore;
using todoapp_server.API.Models;

namespace todoapp_server.API.Data;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options) : base(options) { }

	public DbSet<User> Users { get; set; }
}
