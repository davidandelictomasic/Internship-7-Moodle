using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MoodleApplication.Domain.Entities.Courses;

namespace MoodleApplication.Infrastructure.Database.Configurations.Courses
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("courses");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasColumnName("id");

            builder.Property(c => c.Name)
                   .HasColumnName("name")
                   .IsRequired();

            builder.Property(c => c.ProfessorId)
                   .HasColumnName("professor_id")
                   .IsRequired();



            builder.HasOne(c => c.Professor)
                   .WithMany(p => p.TeachingCourses)
                   .HasForeignKey(c => c.ProfessorId);


        }
    }
}
