using System.Linq.Expressions;

namespace EnigmaApi.Repositories
{
    /// <summary>
    /// Generic interface for generic DAL operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        Task<T?> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetOrCreateBySelector(Expression<Func<T, bool>> selector, Expression<Func<T>> creator);
        void Delete(T entity);
        void Update(T entity);
        void Add(T entity);
        Task SaveChanges();
    }
}
