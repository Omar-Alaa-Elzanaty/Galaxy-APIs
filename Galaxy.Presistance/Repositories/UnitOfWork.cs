using System.Collections;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Presistance.Context;

namespace Pharamcy.Presistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GalaxyDbContext _context;
        private Hashtable _repositries;
        public UnitOfWork(GalaxyDbContext context)
        {
            _context = context;
        }

        public IBaseRepository<T> Repository<T>() where T : class
        {
            if (_repositries == null)
                _repositries = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositries.ContainsKey(type))
            {
                var repositoryType = typeof(BaseRepository<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);

                _repositries.Add(type, repositoryInstance);
            }

            return (IBaseRepository<T>)_repositries[type]!;
        }
        public void Dispose()
        {

            _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
