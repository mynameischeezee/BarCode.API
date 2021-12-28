using DataAccess.Daos;
using DataAccess.Resource;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BarcodeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Scan> Scans { get; set; }
        public DbSet<Role> Roles { get; set; }

        public BarcodeContext(DbContextOptions<BarcodeContext> options)
            : base(options)
        {
            
        }
        
        #region BeforeMigration
        // public BarcodeContext()
        // {
        //     
        // }
        //
        // protected override void OnConfiguring(DbContextOptionsBuilder contextBuilder)
        // {
        //     contextBuilder.UseSqlite(@"DataSource=/Users/nazarkozhin/Desktop/barcode/BarCode.API/DataAccess/Resource/Barcode.db;");
        // }
        #endregion
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new CommentProductConfig());
            modelBuilder.ApplyConfiguration(new UserProductConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
        }
    }
}