namespace MoodleApplication.Domain.Persistence.Common
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        Task CreateTransaction();
        Task Commit();
        Task Rollback();
    }
}
