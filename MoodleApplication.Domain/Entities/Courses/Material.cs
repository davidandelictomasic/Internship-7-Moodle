
namespace MoodleApplication.Domain.Entities.Courses
{
    public class Material
    {
        public int Id { get;  set; }

        public int CourseId { get;  set; }
        public Course Course { get;  set; } = null!;

        public string Name { get;  set; } = null!;
        public string Url { get;  set; } = null!;
        public DateTime AddedAt { get;  set; } = DateTime.UtcNow;


        //public Material(Course course, string name, string url)
        //{
        //    Course = course;
        //    CourseId = course.Id;
        //    Name = name;
        //    Url = url;
        //    AddedAt = DateTime.UtcNow;
        //}
    }
}
