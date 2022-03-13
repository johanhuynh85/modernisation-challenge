using ModernisationChallenge.Domain.SeedWork;

namespace ModernisationChallenge.Domain.TaskAggregate
{
    public class TaskEntity : Entity, IAggregateRoot
    {
        public DateTime? DateCreated { get; private set; }

        public DateTime? DateModified { get; private set; }

        public DateTime? DateDeleted { get; private set; }

        public bool? Completed { get; private set; }

        public string? Details { get; private set; }

        public static TaskEntity Create(string details)
        {
            if (string.IsNullOrEmpty(details))
                throw new ArgumentNullException(nameof(details));

            return new TaskEntity
            {
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Completed = false,
                Details = details
            };
        }

        public void Update(string details, bool completed)
        {
            if (string.IsNullOrEmpty(details))
                throw new ArgumentNullException(nameof(details));

            this.Details = details;
            this.Completed = completed;
            this.DateModified = DateTime.Now;
        }

        public void Delete()
        {
            this.DateDeleted = DateTime.Now;
        }
    }
}
