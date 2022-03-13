using System.ComponentModel.DataAnnotations;
using ModernisationChallenge.Domain.TaskAggregate;

namespace ModernisationChallenge.API.Application.Models
{
    public class TasksPayload<T>
    {
        [Required]
        public T? Task { get; init; }
        public dynamic? Meta { get; init; }

    }

    public class TaskInfo
    {
        public int? Id { get; init; }
        public bool? Completed { get; init; }
        [Required]
        public string? Details { get; init; }
    }

    public static class TaskEntityExtensions
    {
        public static TaskInfo ToTaskInfo(this TaskEntity? data)
        {
            if (data == null)
                return new TaskInfo();

            return new TaskInfo
            {
                Id = data.Id,
                Completed = data.Completed,
                Details = data.Details
            };
        }
    }
}
