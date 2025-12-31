
using MoodleApplication.Domain.Persistence.Users;
using MoodleApplication.Infrastructure.Database;

namespace MoodleApplication.Infrastructure.Persistence.Users
{
    public class UserUnitOfWork : IUserUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IUserRepository Repository { get; }

        public UserUnitOfWork(ApplicationDbContext dbContext, IUserRepository repository)
        {
            _dbContext = dbContext;
            Repository = repository;
        }

        public async Task CreateTransaction()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task Rollback()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
