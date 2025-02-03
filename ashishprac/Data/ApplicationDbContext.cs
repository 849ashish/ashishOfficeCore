using ashishprac.Models;
using Microsoft.EntityFrameworkCore;

namespace ashishprac.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Product> Products { get; set; }


        // Override OnModelCreating to configure the model for isssues  of price giving decimal and if value does not fit to avoid truncate of any issues
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");  // Define precision and scale for the 'Price' column
        }
    }
}
