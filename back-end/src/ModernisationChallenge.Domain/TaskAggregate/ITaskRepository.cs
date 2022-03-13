using ModernisationChallenge.Domain.SeedWork;

namespace ModernisationChallenge.Domain.TaskAggregate
{
    public interface ITaskRepository : IRepository<TaskEntity>
    {
        Task<List<TaskEntity>> GetActiveTasks();
    }
}
