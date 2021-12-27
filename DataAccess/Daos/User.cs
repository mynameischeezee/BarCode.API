using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Daos
{
    public class User : AbstractDao
    {
        public string Name { get; set; }
        
        public string PassHash { get; set; }
        
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Scan> Scans { get; set; }
    }

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasMany(u => u.Scans)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .IsRequired();
        }
    }
}