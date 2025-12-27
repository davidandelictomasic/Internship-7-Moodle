using MoodleApplication.Domain.Entities.Chats;
using MoodleApplication.Domain.Entities.Courses;
using MoodleApplication.Domain.Enumumerations.Users;

namespace MoodleApplication.Domain.Entities.Users
{
    public class User
    {
        public int Id { get; private set; }

        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;

        public string? Name { get; private set; }
        public DateOnly? DateOfBirth { get; private set; }

        public UserRole Role { get; private set; }

        
        public ICollection<Course> Enrollments { get; private set; } = [];
        public ICollection<Course> TeachingCourses { get; private set; } = [];
        public ICollection<Message> SentMessages { get; private set; } = [];
        public ICollection<Message> ReceivedMessages { get; private set; } = [];

        
        public const int NameMaxLength = 50;
        public const int EmailMaxLength = 100;
        public const int PasswordHashMaxLength = 256;

        

        public User(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
            Role = UserRole.Student;
        }

       

        public async Task CreateOrUpdateValidation()
        {
            
        }

        public async Task Create()
        {
            await CreateOrUpdateValidation();
            
        }
    }
}
