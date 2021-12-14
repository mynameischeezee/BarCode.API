using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Daos
{
    public class Scan : AbstractDao
    {
        public DateTime ScanTime { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Comment Comment { get; set; }
    }

    public class UserProductConfig : IEntityTypeConfiguration<Scan>
    {
        public void Configure(EntityTypeBuilder<Scan> builder)
        {
            builder.HasKey(up => up.Id);

            builder.HasOne(s => s.Comment)
                .WithOne(c => c.Scan)
                .HasForeignKey<Comment>(c => c.ScanId);
        }
    }
}