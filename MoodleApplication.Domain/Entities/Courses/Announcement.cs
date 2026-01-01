
using MoodleApplication.Domain.Entities.Users;

namespace MoodleApplication.Domain.Entities.Courses
{
    public class Announcement
    {
        public int Id { get; private set; }

        public int CourseId { get; private set; }
        public Course Course { get; private set; } = null!;

        public int ProfessorId { get; private set; }
        public User Professor { get; private set; } = null!;

        public string Title { get; private set; } = null!;
        public string Content { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;


        //public Announcement(Course course, User professor, string title, string content)
        //{
        //    Course = course;
        //    CourseId = course.Id;
        //    Professor = professor;
        //    ProfessorId = professor.Id;
        //    Title = title;
        //    Content = content;
        //    CreatedAt = DateTime.UtcNow;
        //}
    }
}
