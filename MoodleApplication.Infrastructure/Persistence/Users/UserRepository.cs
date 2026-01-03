using Microsoft.EntityFrameworkCore;
using MoodleApplication.Domain.Entities.Courses;
using MoodleApplication.Domain.Entities.Users;
using MoodleApplication.Domain.Enumumerations.Users;
using MoodleApplication.Domain.Persistence.Users;
using MoodleApplication.Infrastructure.Database;
using MoodleApplication.Infrastructure.Persistence.Common;

namespace MoodleApplication.Infrastructure.Persistence.Users
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ChangeRole(int userId, UserRole newRole)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                user.Role = newRole;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
                
        }

        public async Task<IEnumerable<User>> GetUsersByRole(UserRole role)
        {
            return await _dbContext.Users
                .Where(u => u.Role == role)
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseStudent>> GetStudentEnrollments(int studentId)
        {
            return await _dbContext.CourseStudents
                .Where(cs => cs.UserId == studentId)
                .Include(cs => cs.Course)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetTeachingCourses(int professorId)
        {
            return await _dbContext.Courses
                .Where(c => c.ProfessorId == professorId)
                .ToListAsync();
        }

        public async Task UpdateEmail(int userId, string newEmail)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                user.Email = newEmail;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
