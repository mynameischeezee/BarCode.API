using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Daos
{
    public class Comment : AbstractDao
    {
        public string Data { get; set; }
        public DateTime LeftAt { get; set; }
        public int Rating { get; set; }
        
        public int ScanId { get; set; }
        public virtual Scan Scan { get;set; }

    }

    public class CommentProductConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}