
using MoodleApplication.Domain.Entities.Users;

namespace MoodleApplication.Domain.Entities.Courses
{
    public class CourseStudent
    {
        public int UserId { get; private set; }
        public User Student { get; private set; } = null!;

        public int CourseId { get; private set; }
        public Course Course { get; private set; } = null!;

        public DateTime EnrolledAt { get; private set; } = DateTime.UtcNow;


        //public CourseStudent(User student, Course course)
        //{
        //    Student = student;
        //    UserId = student.Id;
        //    Course = course;
        //    CourseId = course.Id;
        //    EnrolledAt = DateTime.UtcNow;
        //}
    }
}
