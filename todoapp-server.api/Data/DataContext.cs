using Microsoft.EntityFrameworkCore;
using todoapp_server.api.Models;

namespace todoapp_server.api.Data;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options) : base(options) { }

	public DbSet<User> Users { get; set; }
}
