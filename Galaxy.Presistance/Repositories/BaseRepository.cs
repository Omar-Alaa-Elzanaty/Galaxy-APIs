
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Presistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Pharamcy.Presistance.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private GalaxyDbContext _dbContext;

        public BaseRepository(GalaxyDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public Task AddAsync(T input)
        {
            _dbContext.Set<T>().Add(input);
            _dbContext.SaveChanges();
            return Task.CompletedTask;
        }
        public async Task AddRangeAsync(List<T> input)
        {
            await _dbContext.Set<T>().AddRangeAsync(input);
        }
        public Task UpdateAsync(T input)
        {
            _dbContext.Update(input);
            _dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T input)
        {
            _dbContext.Remove(input);

            _dbContext.SaveChanges();
            return Task.CompletedTask;
        }
        public Task DeleteRange(List<T> input)
        {
            _dbContext.RemoveRange(input);
            return Task.CompletedTask;
        }
        public async Task<T> GetItemOnAsync(Func<T, bool> match)
        {
            return _dbContext.Set<T>().FirstOrDefault(match);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public IQueryable<T> Entities() => _dbContext.Set<T>();

        public async Task<IEnumerable<TResult>> GetOnCriteriaAsync<TResult>(Func<T, bool> match, Func<T, TResult> selector)
        {
            return _dbContext.Set<T>().Where(match).Select(selector);
        }
    }
}
