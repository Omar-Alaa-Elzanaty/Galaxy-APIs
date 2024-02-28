
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Presistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Pharamcy.Presistance.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected GalaxyDbContext dbContext;

        public BaseRepository(GalaxyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> GetByIdAsync(int id)
        {

            return dbContext.Set<T>().Find(id);
        }

        public Task AddAsync(T input)
        {
            dbContext.Set<T>().Add(input);
            dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T input)
        {
            dbContext.Update(input);
            dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T input)
        {
            dbContext.Remove(input);
            
            dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<T> GetItemOnAsync(Func<T, bool> match)
        {
            return dbContext.Set<T>().FirstOrDefault(match);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public IQueryable<T> Entities() => dbContext.Set<T>();

        public async Task<IEnumerable<TResult>> GetOnCriteriaAsync<TResult>(Func<T, bool> match, Func<T, TResult> selector)
        {        
            return dbContext.Set<T>().Where(match).Select(selector);        
        }

       
    }
}
