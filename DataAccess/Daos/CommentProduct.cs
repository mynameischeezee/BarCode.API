using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Daos
{
    public class CommentProduct : AbstractDao
    {
        public string Data { get; set; }
        public DateTime LeftAt { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int Rating { get; set; }
    }

    public class CommentProductConfig : IEntityTypeConfiguration<CommentProduct>
    {
        public void Configure(EntityTypeBuilder<CommentProduct> builder)
        {
            builder.HasKey(cp => cp.Id);
            
        }
    }
}