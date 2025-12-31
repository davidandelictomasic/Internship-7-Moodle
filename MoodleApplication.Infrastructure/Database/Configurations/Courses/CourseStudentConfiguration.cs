using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MoodleApplication.Domain.Entities.Courses;

namespace MoodleApplication.Infrastructure.Database.Configurations.Courses
{
    public class CourseStudentConfiguration : IEntityTypeConfiguration<CourseStudent>
    {
        public void Configure(EntityTypeBuilder<CourseStudent> builder)
        {
            builder.ToTable("course_students");

            builder.HasKey(cs => new { cs.UserId, cs.CourseId });

            builder.HasOne(cs => cs.Course)
                .WithMany(c => c.Enrollments) 
                .HasForeignKey(cs => cs.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cs => cs.Student)
                   .WithMany(u => u.Enrollments)
                   .HasForeignKey(cs => cs.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
