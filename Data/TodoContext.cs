using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
public class TodoContext : DbContext
{
	public TodoContext(DbContextOptions<TodoContext> options) : base(options)
	{ }
	public DbSet<TodoItem> TodoItems { get; set; }

}
