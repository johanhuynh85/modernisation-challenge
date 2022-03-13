namespace ModernisationChallenge.Domain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task<T> AddItem(T item);
        Task<T> UpdateItem(T item);
        Task<T?> GetItem(int id);
        Task<bool> DeleteItem(T item);

        IUnitOfWork UnitOfWork { get; }
    }
}
