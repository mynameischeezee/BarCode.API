using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Daos
{
    public class Product
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int OverallRatingSum { get; set; }
        public int CountOfRatings { get; set; }

        public virtual ICollection<CommentProduct> Comments { get; set; }
        public virtual ICollection<UserProduct> Scans { get; set; }
    }

    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Code);

            builder.HasMany(p => p.Comments)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId)
                .IsRequired();

            builder.HasMany(p => p.Scans)
                .WithOne(s => s.Product)
                .HasForeignKey(s => s.ProductId)
                .IsRequired();
        }
    }
}