using Microsoft.EntityFrameworkCore;

namespace product.stock.api
{
    public class StockContext : DbContext
    {               
        public StockContext() { }

        public StockContext(DbContextOptions<StockContext> options) : base(options){}

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("product");
            modelBuilder.Entity<Product>().HasKey(c => c.Id);
            modelBuilder.Entity<Product>().Property(c => c.Id).HasColumnName("id").HasColumnType("BIGINT");
            modelBuilder.Entity<Product>().Property(c => c.Description).HasColumnName("description").HasMaxLength(5000); ;
            modelBuilder.Entity<Product>().Property(c => c.Quantity).HasColumnName("quantity").HasColumnType("FLOAT");
            modelBuilder.Entity<Product>().Property(c => c.Unit).HasColumnName("unit");
            modelBuilder.Entity<Product>().Property(c => c.Category).HasColumnName("category");            
            modelBuilder.Entity<Product>().Property(c => c.Inserted).HasColumnName("inserted").HasColumnType("DATETIME");
            modelBuilder.Entity<Product>().Property(c => c.Modified).HasColumnName("modified").HasColumnType("DATETIME");

            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<User>().HasKey(c => c.Id);
            modelBuilder.Entity<User>().Property(c => c.Id).HasColumnName("id").HasColumnType("INT");
            modelBuilder.Entity<User>().Property(c => c.Login).HasColumnName("login").HasMaxLength(500);
            modelBuilder.Entity<User>().Property(c => c.Pass).HasColumnName("pass").HasMaxLength(500);
            modelBuilder.Entity<User>().Property(c => c.Token).HasColumnName("token");
            modelBuilder.Entity<User>().Property(c => c.Validity).HasColumnName("validity").HasColumnType("DATETIME");

            base.OnModelCreating(modelBuilder);
        }
    }
}
