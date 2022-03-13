using System;
using ModernisationChallenge.Domain.TaskAggregate;
using Xunit;

namespace ModernisationChallenge.UnitTests.Domain
{
    public class TaskAggregateTests
    {
        [Fact]
        public void Create_Task_Success()
        {
            //Arrange
            string sampleDetails = "Sample testing details";

            //Act 
            var taskEntity = TaskEntity.Create(sampleDetails);

            //Assert
            Assert.NotNull(taskEntity);
            Assert.Equal(taskEntity.Details, sampleDetails);
            Assert.NotNull(taskEntity.DateCreated);
            Assert.NotNull(taskEntity.DateModified);
            Assert.NotNull(taskEntity.Completed);
            Assert.False(taskEntity.Completed);
        }

        [Fact]
        public void Create_Task_Failure()
        {
            //Arrange
            string sampleDetails = "";

            //Act - Assert
            Assert.Throws<ArgumentNullException>(() => TaskEntity.Create(sampleDetails));
        }

        [Theory]
        [InlineData("Sample testing details", false)]
        [InlineData("Sample testing details", true)]
        public void Update_Task_Success(string details, bool completed)
        {
            //Arrange
            var taskEntity = TaskEntity.Create("test");

            //Act 
            taskEntity.Update(details, completed);

            //Assert
            Assert.NotNull(taskEntity);
            Assert.Equal(taskEntity.Details, details);
            Assert.NotNull(taskEntity.DateModified);
            Assert.NotNull(taskEntity.Completed);
            Assert.Equal(completed, taskEntity.Completed);
        }

        [Fact]
        public void Update_Task_Failure()
        {
            //Arrange
            var taskEntity = TaskEntity.Create("test");

            //Act - Assert
            Assert.Throws<ArgumentNullException>(() => taskEntity.Update("", false));
        }

        [Fact]
        public void Delete_Task_Success()
        {
            //Arrange
            string sampleDetails = "Sample testing details";

            //Act 
            var taskEntity = TaskEntity.Create(sampleDetails);
            taskEntity.Delete();

            //Assert
            Assert.NotNull(taskEntity);
            Assert.NotNull(taskEntity.DateDeleted);
        }
    }
}
