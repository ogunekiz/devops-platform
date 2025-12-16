using DevOpsPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevOpsPlatform.Infrastructure.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
				: base(options) { }

		public DbSet<Order> Orders => Set<Order>();
	}
}
