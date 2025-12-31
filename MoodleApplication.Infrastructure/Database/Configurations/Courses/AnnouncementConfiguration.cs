using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MoodleApplication.Domain.Entities.Courses;

namespace MoodleApplication.Infrastructure.Database.Configurations.Courses
{
    public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder.ToTable("announcements");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                   .HasColumnName("id");

            builder.Property(m => m.CourseId)
                   .HasColumnName("course_id")
                   .IsRequired();

            builder.Property(m => m.ProfessorId)
                   .HasColumnName("professor_id")
                   .IsRequired();

            builder.Property(m => m.Title)
                   .HasColumnName("title")
                   .IsRequired();

            builder.Property(m => m.Content)
                   .HasColumnName("content")
                   .IsRequired();
            builder.Property(a => a.CreatedAt)
                   .HasColumnName("created_at")
                   .IsRequired();

            builder.HasOne(m => m.Course)
                   .WithMany(c => c.Announcements)
                   .HasForeignKey(m => m.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
