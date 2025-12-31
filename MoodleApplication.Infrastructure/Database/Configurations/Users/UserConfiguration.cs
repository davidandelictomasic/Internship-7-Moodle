using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoodleApplication.Domain.Entities.Users;

namespace MoodleApplication.Infrastructure.Database.Configurations.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .HasColumnName("id");

            builder.Property(u => u.Name)
                   .HasColumnName("name");

            builder.Property(u => u.Email)
                   .HasColumnName("email")
                   .IsRequired();

            builder.Property(u => u.PasswordHash)
                   .HasColumnName("password")
                   .IsRequired();

            builder.Property(u => u.DateOfBirth)
                   .HasColumnName("date_of_birth");

            builder.Property(u => u.Role)
                   .HasColumnName("role")
                   .IsRequired();

            builder.HasIndex(u => u.Email)
                   .IsUnique();
        }
    }
}
