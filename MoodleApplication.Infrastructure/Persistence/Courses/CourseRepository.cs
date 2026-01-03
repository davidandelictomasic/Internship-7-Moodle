using Microsoft.EntityFrameworkCore;
using MoodleApplication.Domain.Entities.Courses;
using MoodleApplication.Domain.Entities.Users;
using MoodleApplication.Domain.Enumumerations.Users;
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

        public async Task AddAnnouncement(int courseId, Announcement announcement)
        {
            announcement.CourseId = courseId;
            announcement.CreatedAt = DateTime.UtcNow;
            await _dbContext.Announcements.AddAsync(announcement);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddMaterial(int courseId, Material material)
        {
            material.CourseId = courseId;
            material.AddedAt = DateTime.UtcNow;
            await _dbContext.Materials.AddAsync(material);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddStudentToCourse(int courseId, int studentId)
        {
            var enrollment = new CourseStudent
            {
                CourseId = courseId,
                UserId = studentId
            };
            await _dbContext.CourseStudents.AddAsync(enrollment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetStudentsNotInCourse(int courseId)
        {
            var enrolledStudentIds = await _dbContext.CourseStudents
                .Where(cs => cs.CourseId == courseId)
                .Select(cs => cs.UserId)
                .ToListAsync();

            return await _dbContext.Users
                .Where(u => u.Role == UserRole.Student && !enrolledStudentIds.Contains(u.Id))
                .OrderBy(u => u.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await _dbContext.Courses.ToListAsync();
        }

        public async Task<Course?> GetById(int courseId)
        {
            return await _dbContext.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
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
