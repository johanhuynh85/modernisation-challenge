using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ModernisationChallenge.API.Application.Models;
using ModernisationChallenge.Domain.SeedWork;
using ModernisationChallenge.Domain.TaskAggregate;
using Moq;
using Xunit;

namespace ModernisationChallenge.IntegrationTests.Controllers
{
    public class TasksControllerTests : ControllerTestBase
    {
        private const string _TasksBaseUri = "/api/Tasks";

        public TasksControllerTests(
            WebApplicationFactory<Program> factory)
        : base(factory) { }

        [Fact]
        public async Task Get_AvailableTasks_Success()
        {
            // Arrange
            var taskRepoMock = new Mock<ITaskRepository>();

            var data = new List<TaskEntity>
            {
                TaskEntity.Create("Available Task 1"),
                TaskEntity.Create("Available Task 2"),
                TaskEntity.Create("Available Task 3"),
            };

            taskRepoMock.Setup(x => x.GetActiveTasks())
                .ReturnsAsync(data);
            var client = CreateClient((services) =>
            {
                services.AddScoped<ITaskRepository>((x) => taskRepoMock.Object);
            });

            //Act
            var response = await client.GetAsync($"{_TasksBaseUri}");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(QueryDataset))]
        public async Task Get_TaskById_Success(int existingTaskId, int queryTaskId, HttpStatusCode result)
        {
            // Arrange
            var taskRepoMock = new Mock<ITaskRepository>();

            var data = TaskEntity.Create($"Task {existingTaskId}");

            taskRepoMock.Setup(x => x.GetItem(
                    It.Is<int>(x => x == existingTaskId)
                ))
                .ReturnsAsync(data);
            var client = CreateClient((services) =>
            {
                services.AddScoped<ITaskRepository>((x) => taskRepoMock.Object);
            });

            //Act
            var response = await client.GetAsync($"{_TasksBaseUri}/{queryTaskId}");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(result, response.StatusCode);
        }

        [Fact]
        public async Task Get_TaskById_Failure()
        {
            // Arrange
            var taskRepoMock = new Mock<ITaskRepository>();

            var data = TaskEntity.Create("Task Id");

            taskRepoMock.Setup(x => x.GetItem(
                    It.IsAny<int>()
                ))
                .ReturnsAsync(data);
            var client = CreateClient((services) =>
            {
                services.AddScoped<ITaskRepository>((x) => taskRepoMock.Object);
            });

            //Act
            var response = await client.GetAsync($"{_TasksBaseUri}/error");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(CommandDataset))]
        public async Task Create_Task_Success(int existingTaskId, TaskInfo taskInfo, HttpStatusCode result)
        {
            // Arrange
            var taskRepoMock = new Mock<ITaskRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var data = TaskEntity.Create($"Task {existingTaskId}");

            unitOfWorkMock.Setup(x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(1);

            taskRepoMock.Setup(x => x.UnitOfWork)
                .Returns(unitOfWorkMock.Object);
            taskRepoMock.Setup(x => x.AddItem(
                    It.IsAny<TaskEntity>()
                ))
                .ReturnsAsync(data);
            var client = CreateClient((services) =>
            {
                services.AddScoped<ITaskRepository>((x) => taskRepoMock.Object);
            });

            //Act
            string jsonData = JsonSerializer.Serialize(new TasksPayload<TaskInfo> { Task = taskInfo });
            var response = await client.PostAsync($"{_TasksBaseUri}",
                new StringContent(jsonData, Encoding.UTF8, "application/json")
            );

            //Assert
            Assert.NotNull(response);
            Assert.Equal(result, response.StatusCode);

            Times verify = Times.Once();
            taskRepoMock.Verify(x => x.AddItem(
                    It.IsAny<TaskEntity>()
                ), verify);

            unitOfWorkMock.Verify(x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()
                ), verify);
        }

        [Theory]
        [MemberData(nameof(CommandDataset))]
        public async Task Update_Task_Success(int existingTaskId, TaskInfo updateTask, HttpStatusCode result)
        {
            // Arrange
            var taskRepoMock = new Mock<ITaskRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var data = TaskEntity.Create($"Task {existingTaskId}");

            unitOfWorkMock.Setup(x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(1);

            taskRepoMock.Setup(x => x.UnitOfWork)
                .Returns(unitOfWorkMock.Object);
            taskRepoMock.Setup(x => x.GetItem(
                    It.Is<int>(x => x == existingTaskId)
                ))
                .ReturnsAsync(data);
            taskRepoMock.Setup(x => x.UpdateItem(
                    It.IsAny<TaskEntity>()
                ))
                .ReturnsAsync(data);
            var client = CreateClient((services) =>
            {
                services.AddScoped<ITaskRepository>((x) => taskRepoMock.Object);
            });

            //Act
            string jsonData = JsonSerializer.Serialize(new TasksPayload<TaskInfo> { Task = updateTask });
            var response = await client.PutAsync($"{_TasksBaseUri}/{updateTask.Id}",
                new StringContent(jsonData, Encoding.UTF8, "application/json")
            );

            //Assert
            Assert.NotNull(response);
            Assert.Equal(result, response.StatusCode);

            Times verify = existingTaskId == updateTask.Id ? Times.Once() : Times.Never();
            taskRepoMock.Verify(x => x.UpdateItem(
                    It.IsAny<TaskEntity>()
                ), verify);

            unitOfWorkMock.Verify(x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()
                ), verify);
        }

        [Theory]
        [MemberData(nameof(CommandDataset))]
        public async Task Delete_Task_Success(int existingTaskId, TaskInfo taskInfo, HttpStatusCode result)
        {
            // Arrange
            var taskRepoMock = new Mock<ITaskRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var data = TaskEntity.Create(taskInfo.Details);

            unitOfWorkMock.Setup(x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(1);

            taskRepoMock.Setup(x => x.UnitOfWork)
                .Returns(unitOfWorkMock.Object);
            taskRepoMock.Setup(x => x.GetItem(
                    It.Is<int>(x => x == existingTaskId)
                ))
                .ReturnsAsync(data);
            taskRepoMock.Setup(x => x.UpdateItem(
                    It.IsAny<TaskEntity>()
                ))
                .ReturnsAsync(data);
            var client = CreateClient((services) =>
            {
                services.AddScoped<ITaskRepository>((x) => taskRepoMock.Object);
            });

            //Act
            var response = await client.DeleteAsync($"{_TasksBaseUri}/{taskInfo.Id}");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(result, response.StatusCode);

            Times verify = existingTaskId == taskInfo.Id ? Times.Once() : Times.Never();
            taskRepoMock.Verify(x => x.UpdateItem(
                    It.IsAny<TaskEntity>()
                ), verify);

            unitOfWorkMock.Verify(x => x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()
                ), verify);
        }

        public static readonly TheoryData<int, TaskInfo, HttpStatusCode> CommandDataset = new TheoryData<int, TaskInfo, HttpStatusCode>
        {
            {
                123, 
                new TaskInfo { Id = 123, Details = "Update Task Details", Completed = false }, 
                HttpStatusCode.OK
            },
            {
                123,
                new TaskInfo { Id = 123, Details = "Complete Task", Completed = true },
                HttpStatusCode.OK
            },
            {
                123,
                new TaskInfo { Id = 456, Details = "Not Found Task", Completed = false },
                HttpStatusCode.OK
            }
        };

        public static readonly TheoryData<int, int, HttpStatusCode> QueryDataset = new TheoryData<int, int, HttpStatusCode>
        {
            {
                123,
                123,
                HttpStatusCode.OK
            },
            {
                123,
                456,
                HttpStatusCode.OK
            }
        };
    }
}
