using System.Linq.Expressions;
using EnigmaApi.Data_Access;
using Microsoft.EntityFrameworkCore;

namespace EnigmaApi.Shared.Repositories
{
    /// <summary>
    /// Implementation of Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericEfCoreRepository<T> : IRepository<T> where T : class
    {
        private readonly EnigmaDbContext _context;
        private DbSet<T> _dbSet;

        public GenericEfCoreRepository(EnigmaDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<T?> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Gets an entity by a selector or creates it using the provided creator expression if it does not exist.
        /// </summary>
        /// <param name="selector">The expression to select the entity.</param>
        /// <param name="creator">The expression to create a new entity if it does not exist.</param>
        /// <returns>The existing or newly created entity.</returns>
        public async Task<T> GetOrCreateBySelector(Expression<Func<T, bool>> selector, Expression<Func<T>> creator)
        {
            var item = await _dbSet.FirstOrDefaultAsync(selector);
            if (item == null)
            {
                var createItem = creator.Compile();
                item = createItem.Invoke();
                _dbSet.Add(item);
            }

            return item;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
