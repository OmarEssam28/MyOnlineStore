using Microsoft.EntityFrameworkCore;
using MyOnlineStore.Models;

// This class is the bridge between your application and the database.
// We've named it MyContext as you requested.
namespace MyOnlineStore.Data
{
    public class MyContext : DbContext
    {
        // The OnConfiguring method allows you to configure the database connection
        // directly within the context class.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // We specify the connection string here.
            // This is another way to connect to the database, often used for simplicity.
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=OmarEssam;Database=MyOnlineStoreDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        // Create a DbSet for each model. This tells EF Core to create a table for each.
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
