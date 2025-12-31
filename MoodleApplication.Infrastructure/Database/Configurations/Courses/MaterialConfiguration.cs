

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MoodleApplication.Domain.Entities.Chats;
using MoodleApplication.Domain.Entities.Courses;

namespace MoodleApplication.Infrastructure.Database.Configurations.Courses
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("materials");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                   .HasColumnName("id");

            builder.Property(m => m.CourseId)
                   .HasColumnName("course_id")
                   .IsRequired();

            builder.Property(m => m.Name)
                   .HasColumnName("name")
                   .IsRequired();

            builder.Property(m => m.AddedAt)
                   .HasColumnName("added_at")
                   .IsRequired();

            builder.Property(m => m.Url)
                   .HasColumnName("url")
                   .IsRequired();

            builder.HasOne(m => m.Course)
                   .WithMany(c => c.Materials)
                   .HasForeignKey(m => m.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}
