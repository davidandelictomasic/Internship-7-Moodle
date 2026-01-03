
using MoodleApplication.Domain.Entities.Users;

namespace MoodleApplication.Domain.Entities.Courses
{
    public class CourseStudent
    {
        public int UserId { get;  set; }
        public User Student { get;  set; } = null!;

        public int CourseId { get;  set; }
        public Course Course { get;  set; } = null!;

        public DateTime EnrolledAt { get;  set; } = DateTime.UtcNow;

       
    }
}
