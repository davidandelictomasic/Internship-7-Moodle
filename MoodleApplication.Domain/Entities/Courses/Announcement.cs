
using MoodleApplication.Domain.Entities.Users;

namespace MoodleApplication.Domain.Entities.Courses
{
    public class Announcement
    {
        public int Id { get;  set; }

        public int CourseId { get;  set; }
        public Course Course { get;  set; } = null!;

        public int ProfessorId { get;  set; }
        public User Professor { get;  set; } = null!;

        public string Title { get;  set; } = null!;
        public string Content { get;  set; } = null!;
        public DateTime CreatedAt { get;  set; } = DateTime.UtcNow;


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
