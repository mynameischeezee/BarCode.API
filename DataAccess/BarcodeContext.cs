using DataAccess.Daos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BarcodeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Scan> Scans { get; set; }

        #region BeforeMigration
        public BarcodeContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder contextBuilder)
        {
            contextBuilder.UseSqlite("jdbc:sqlite:identifier.sqlite");
        }
        #endregion
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new CommentProductConfig());
            modelBuilder.ApplyConfiguration(new UserProductConfig());
        }
    }
}