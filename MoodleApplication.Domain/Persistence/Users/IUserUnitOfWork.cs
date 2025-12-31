using MoodleApplication.Domain.Persistence.Common;

namespace MoodleApplication.Domain.Persistence.Users
{
    public interface IUserUnitOfWork : IUnitOfWork
    {
        IUserRepository UserRepository { get; }

    }
}
