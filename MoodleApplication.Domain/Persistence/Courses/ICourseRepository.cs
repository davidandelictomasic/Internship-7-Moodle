using MoodleApplication.Domain.Entities.Courses;
using MoodleApplication.Domain.Entities.Users;
using MoodleApplication.Domain.Persistence.Common;

namespace MoodleApplication.Domain.Persistence.Courses
{
    public interface ICourseRepository : IRepository<Course, int>
    {
        Task<Course?> GetById(int courseId);
        Task<IEnumerable<Course>> GetAllCourses();

        Task AddStudentToCourse(int courseId, int studentId);
        Task<IEnumerable<User>> GetStudentsForCourse(int courseId);
        Task<IEnumerable<User>> GetStudentsNotInCourse(int courseId);

        Task AddAnnouncement(int courseId, Announcement announcement);
        Task AddMaterial(int courseId, Material material);

        Task<IEnumerable<Announcement>> GetCourseAnnouncements(int courseId);
        Task<IEnumerable<Material>> GetCourseMaterials(int courseId);
    }
}
