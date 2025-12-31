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

        public Task ChangeRole(int userId, UserRole newRole)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
                
        }

        public Task<IEnumerable<CourseStudent>> GetStudentEnrollments(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Course>> GetTeachingCourses(int professorId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEmail(int userId, string newEmail)
        {
            throw new NotImplementedException();
        }
    }
}
