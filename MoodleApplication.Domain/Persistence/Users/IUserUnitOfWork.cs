using MoodleApplication.Domain.Persistence.Chats;
using MoodleApplication.Domain.Persistence.Common;
using MoodleApplication.Domain.Persistence.Courses;

namespace MoodleApplication.Domain.Persistence.Users
{
    public interface IUserUnitOfWork : IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICourseRepository CourseRepository { get; }


    }
}
