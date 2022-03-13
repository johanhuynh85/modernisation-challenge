using Microsoft.EntityFrameworkCore;
using ModernisationChallenge.Domain.TaskAggregate;

namespace ModernisationChallenge.Infrastructure.Repositories
{
    public class TaskRepository : GenericRepository<TaskEntity>, ITaskRepository
    {
        public TaskRepository(ModernisationDbContext context) : base(context)
        {
        }

        public Task<List<TaskEntity>> GetActiveTasks()
        {
            return _context.Tasks
                .Where(x => x.DateDeleted == null)
                .OrderBy(y => y.Id)
                .ToListAsync();
        }
    }
}
