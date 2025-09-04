using ScooterApp.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace ScooterApp.DataAccess
{
	public class ScooterDbContext : DbContext
	{
		public DbSet<Scooter> Scooter => Set<Scooter>();
		public DbSet<User> User => Set<User>();
		public DbSet<Trip> Trip => Set<Trip>();
		public DbSet<LocationEvent> LocationEvent => Set<LocationEvent>();
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder
			.UseNpgsql(
			"Host = localhost:5432; " +
			"Username = corgi; " +
			"Password = welsh; " +
			"Database = pembroke;")
			.UseLowerCaseNamingConvention();
		}
	}
}
