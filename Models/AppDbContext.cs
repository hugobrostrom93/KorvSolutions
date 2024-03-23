using Microsoft.EntityFrameworkCore;

namespace KorvSolutions.Models
{
    public class AppDbContext : DbContext
    {
        // Add constructor to accept DbContextOptions
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanProduct> PlanProducts { get; set; } // DbSet for PlanProduct table

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the connection string to your SQL Server database
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=KorvSolutionsDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PlanProduct>()
                .HasKey(pp => new { pp.PlanId, pp.ProductId });

            // Define relationships between models if needed
            modelBuilder.Entity<Ingredient>()
                .HasOne(p => p.Product)
                .WithMany(i => i.Ingredients)
                .HasForeignKey(p => p.ProductId);

            // Call base method
            base.OnModelCreating(modelBuilder);
        }
    }
}