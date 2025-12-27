using MoodleApplication.Domain.Entities.Users;

namespace MoodleApplication.Domain.Entities.Courses
{
    public class Course
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = null!;

        public int ProfessorId { get; private set; }
        public User Professor { get; private set; } = null!;

        public ICollection<CourseStudent> Enrollments { get; private set; } = [];
        public ICollection<Announcement> Announcements { get; private set; } = [];
        public ICollection<Material> Materials { get; private set; } = [];


        public Course(string name, User professor)
        {
            Name = name;
            Professor = professor;
            ProfessorId = professor.Id;
        }
    }
}
