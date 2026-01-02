using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoodleApplication.Domain.Entities.Chats;
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

            builder.HasMany(u => u.SentMessages)
                   .WithOne(m => m.Sender)
                   .HasForeignKey(m => m.SenderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<ChatRoom>()
                   .WithOne(cr => cr.FirstUser)
                   .HasForeignKey(cr => cr.FirstUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<ChatRoom>()
                   .WithOne(cr => cr.SecondUser)
                   .HasForeignKey(cr => cr.SecondUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Enrollments)
                   .WithOne(cs => cs.Student)
                   .HasForeignKey(cs => cs.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
