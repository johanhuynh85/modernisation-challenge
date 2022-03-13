using Microsoft.AspNetCore.Mvc;
using ModernisationChallenge.API.Application.Models;
using ModernisationChallenge.Domain.TaskAggregate;

namespace ModernisationChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<TasksPayload<List<TaskInfo>>> Get()
        {
            return new TasksPayload<List<TaskInfo>>
            {
                Task = (await _taskRepository.GetActiveTasks())
                    .Select(x => x.ToTaskInfo()).ToList()
            };
        }

        [HttpGet("{id}")]
        public async Task<TasksPayload<TaskInfo>> Get(int id)
        {
            var result = await _taskRepository.GetItem(id);
            return new TasksPayload<TaskInfo>
            {
                Task = result.ToTaskInfo()
            };
        }

        [HttpPost]
        public async Task<TasksPayload<TaskInfo>> Post([FromBody] TasksPayload<TaskInfo> data)
        {
            var result = await _taskRepository.AddItem(TaskEntity.Create(data.Task.Details));
            await _taskRepository.UnitOfWork.SaveChangesAsync();

            return new TasksPayload<TaskInfo>
            {
                Task = result.ToTaskInfo()
            };
        }

        [HttpPut("{id}")]
        public async Task<TasksPayload<TaskInfo>> Put(int id, TasksPayload<TaskInfo> data)
        {
            var taskEntity = await _taskRepository.GetItem(id);
            if (taskEntity != null)
            {
                taskEntity.Update(data.Task.Details, data.Task.Completed ?? false);

                await _taskRepository.UpdateItem(taskEntity);
                await _taskRepository.UnitOfWork.SaveChangesAsync();
            }
            return new TasksPayload<TaskInfo>
            {
                Task = taskEntity.ToTaskInfo()
            };
        }

        [HttpDelete("{id}")]
        public async Task<TasksPayload<TaskInfo>> Delete(int id)
        {
            var taskEntity = await _taskRepository.GetItem(id);
            if (taskEntity != null)
            {
                taskEntity.Delete();

                await _taskRepository.UpdateItem(taskEntity);
                await _taskRepository.UnitOfWork.SaveChangesAsync();
            }

            return new TasksPayload<TaskInfo>
            {
                Task = taskEntity.ToTaskInfo()
            };
        }
    }
}
