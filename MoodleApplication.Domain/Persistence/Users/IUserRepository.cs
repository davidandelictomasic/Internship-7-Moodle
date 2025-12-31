using MoodleApplication.Domain.Entities.Courses;
using MoodleApplication.Domain.Entities.Users;
using MoodleApplication.Domain.Enumumerations.Users;
using MoodleApplication.Domain.Persistence.Common;

namespace MoodleApplication.Domain.Persistence.Users
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User?> GetById(int id);
        Task<User?> GetByEmail(string email);
        Task<IEnumerable<User>> GetAllUsers();

        
        Task UpdateEmail(int userId, string newEmail);
        Task ChangeRole(int userId, UserRole newRole);
        Task DeleteUser(int userId);

        Task<IEnumerable<CourseStudent>> GetStudentEnrollments(int studentId);
        Task<IEnumerable<Course>> GetTeachingCourses(int professorId);
    }

}

