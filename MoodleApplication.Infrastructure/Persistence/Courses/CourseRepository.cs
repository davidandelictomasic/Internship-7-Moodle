using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Announcement>> GetCourseAnnouncements(int courseId)
        {
            return await _dbContext.Announcements
                 .Where(a => a.CourseId == courseId)
                 .Include(a => a.Professor)
                 .OrderByDescending(a => a.CreatedAt)
                 .ToListAsync();

        }

        public async Task<IEnumerable<Material>> GetCourseMaterials(int courseId)
        {
            return await _dbContext.Materials
                 .Where(a => a.CourseId == courseId)
                 .OrderByDescending(m => m.AddedAt)
                 .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetStudentsForCourse(int courseId)
        {
            return await _dbContext.CourseStudents
                .Where(cs => cs.CourseId == courseId)
                .Include(cs => cs.Student)
                .OrderBy(cs => cs.Student.Name)
                .Select(cs => cs.Student)
                .ToListAsync();
        }
    }
}
