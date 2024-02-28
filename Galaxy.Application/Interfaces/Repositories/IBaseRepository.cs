namespace Galaxy.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T:class
    {
        //add-delete-update-getbyid

        public  Task<T> GetByIdAsync(int id);
        public  Task<T> GetItemOnAsync(Func<T,bool>match);
        //public  Task<IEnumerable<T>> GetAllByIdAsync(Func<T,bool>match);
        public  Task<IEnumerable<TResult>> GetOnCriteriaAsync<TResult>(Func<T,bool>match,Func<T,TResult>selector);

        public  Task<IEnumerable<T>> GetAllAsync();

        public IQueryable<T> Entities();
        public Task AddAsync(T input);

        public Task UpdateAsync(T input);

        public Task DeleteAsync(T input);
    }
}
