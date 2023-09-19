using Microsoft.EntityFrameworkCore;
using Domain;
namespace Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
			base(options)
		{
		}

		public DbSet<Property> properties => Set<Property>(); // { get; set; }

		public DbSet<Image> images => Set<Image>();
	}
}

