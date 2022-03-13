using ModernisationChallenge.Domain.SeedWork;

namespace ModernisationChallenge.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> where T : Entity
    {
        protected readonly ModernisationDbContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public GenericRepository(ModernisationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<T> AddItem(T item)
        {
            var result = await _context.AddAsync<T>(item);
            return result.Entity;
        }

        public Task<bool> DeleteItem(T item)
        {
            _context.Remove<T>(item);
            
            // haven't committed change yet
            return Task.FromResult(true); 
        }

        public async Task<T?> GetItem(int id)
        {
            return await _context.FindAsync<T>(id);
        }

        public Task<T> UpdateItem(T item)
        {
            var result = _context.Update<T>(item);
            return Task.FromResult(result.Entity);
        }
    }
}
