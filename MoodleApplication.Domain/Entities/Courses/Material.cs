
namespace MoodleApplication.Domain.Entities.Courses
{
    public class Material
    {
        public int Id { get; private set; }

        public int CourseId { get; private set; }
        public Course Course { get; private set; } = null!;

        public string Name { get; private set; } = null!;
        public string Url { get; private set; } = null!;
        public DateTime AddedAt { get; private set; } = DateTime.UtcNow;


        public Material(Course course, string name, string url)
        {
            Course = course;
            CourseId = course.Id;
            Name = name;
            Url = url;
            AddedAt = DateTime.UtcNow;
        }
    }
}
