using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoodleApplication.Domain.Entities.Courses;
using MoodleApplication.Domain.Entities.Users;
using MoodleApplication.Domain.Persistence.Courses;
using MoodleApplication.Infrastructure.Database;
using MoodleApplication.Infrastructure.Persistence.Common;

namespace MoodleApplication.Infrastructure.Persistence.Chats
{
    public class CourseRepository : Repository<Course, int>, ICourseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CourseRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAnnouncement(int courseId, Announcement announcement)
        {
            throw new NotImplementedException();
        }

        public Task AddMaterial(int courseId, Material material)
        {
            throw new NotImplementedException();
        }

        public Task AddStudentToCourse(int courseId, int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Course>> GetAllCourses()
        {
            throw new NotImplementedException();
        }

        public Task<Course?> GetById(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Announcement>> GetCourseAnnouncements(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Material>> GetCourseMaterials(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetStudentsForCourse(int courseId)
        {
            throw new NotImplementedException();
        }
    }
}
