using MoodleApplication.Domain.Entities.Users;
using MoodleApplication.Domain.Persistence.Common;

namespace MoodleApplication.Domain.Persistence.Users
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User?> GetById(int id);

    }
}
