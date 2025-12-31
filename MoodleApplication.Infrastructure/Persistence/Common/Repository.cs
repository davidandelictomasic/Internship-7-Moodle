
using Microsoft.EntityFrameworkCore;
using MoodleApplication.Domain.Common.Model;
using MoodleApplication.Domain.Persistence.Common;

namespace MoodleApplication.Infrastructure.Persistence.Common
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<GetAllResponse<TEntity>> Get()
        {
            var entities = await _dbSet.ToListAsync();
            return new GetAllResponse<TEntity> { Values = entities };
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void Delete(TEntity? entity)
        {
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
    }
}
