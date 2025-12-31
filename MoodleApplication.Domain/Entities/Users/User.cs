using MoodleApplication.Domain.Common.Model;
using MoodleApplication.Domain.Common.Validation;
using MoodleApplication.Domain.Common.Validation.ValidationItems;
using MoodleApplication.Domain.Entities.Chats;
using MoodleApplication.Domain.Entities.Courses;
using MoodleApplication.Domain.Enumumerations.Users;
using MoodleApplication.Domain.Persistence.Users;

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


        public ICollection<CourseStudent> Enrollments { get; private set; } = [];
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



        public async Task<Result<bool>> Create(IUserRepository userRepository)
        {
            var validationResult = await CreateOrUpdateValidation(userRepository);
            if (validationResult.HasError)
                return new Result<bool>(false, validationResult);
            await userRepository.InsertAsync(this);
            return new Result<bool>(true, validationResult);


        }
        public async Task<ValidationResult> CreateOrUpdateValidation(IUserRepository userRepository)
        {
            var validationResult = new ValidationResult();
            if (Name?.Length > NameMaxLength)
                validationResult.AddValidationItem(UserValidationItems.NameMaxLength);

            return validationResult;

        }
        public async Task<Result<bool>> Update(IUserRepository userRepository)
        {
            var validationResult = await CreateOrUpdateValidation(userRepository);
            if (validationResult.HasError)
                return new Result<bool>(false, validationResult);

            userRepository.Update(this);
            return new Result<bool>(true, validationResult);
        }
    }
}
