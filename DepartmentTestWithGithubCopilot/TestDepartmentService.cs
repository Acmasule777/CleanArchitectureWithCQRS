using Department.Application.Commands.Department;
using Department.Application.Interfaces;
using Department.Application.Validation;
using Department.Infrastructure.Persistency;
using Department.Infrastructure.Repositories;
using DepartmentCore.Core.DTOs;
using DepartmentCore.Core.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DepartmentTestWithGithubCopilot
{
    public class TestDepartmentService
    {
        private readonly CreateDepartmentValidationCommand _validator = new();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CreateDepartmentValidator_Fails_For_Null_Empty_Or_Whitespace_Name(string? name)
        {
            // Arrange
            var command = new createDepartmentCommand(new DepartmentDto
            {
                DepartmentName = name
            });
            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Dto.DepartmentName");
        }

        [Theory]
        [InlineData("Sales", "Department Updated Successfully")]
        public async Task UpdateDepartment_ReturnsSuccessMessage_WithMockedContext_OnlyChecksString(string departmentName, string expectedMessage)
        {
            // Arrange
            var mockRepo = new Mock<IDepartment>();

            mockRepo
                .Setup(r => r.UpdateDepartment(It.Is<UpdateDepartmentDto>(d => d.DepartmentName == departmentName)))
                .ReturnsAsync(expectedMessage);

            var updateDto = new UpdateDepartmentDto
            {
                DepartmentId = 1,
                DepartmentName = departmentName
            };

            // Act
            var result = await mockRepo.Object.UpdateDepartment(updateDto);

            // Assert
            Assert.Equal(expectedMessage, result);
            mockRepo.Verify(r => r.UpdateDepartment(It.IsAny<UpdateDepartmentDto>()), Times.Once);
        }


        [Fact]
      
        public async Task DeleteDepartment_ReturnsNotFound_WhenEntityDoesNotExist_MockedContext()
        {
            // Arrange
            var mockSet = new Mock<DbSet<DepartmentEntity>>();

            // Setup FindAsync to return null (not found)
            mockSet
                .Setup(m => m.FindAsync(It.IsAny<object[]>()))
                .Returns<object[]>(ids => new ValueTask<DepartmentEntity?>(Task.FromResult<DepartmentEntity?>(null)));

            var mockContext = new Mock<AppDepartmentDbContext>(new DbContextOptions<AppDepartmentDbContext>());

            // IMPORTANT: assign the mocked DbSet to the real mock object instance.
            // Using Setup(c => c.Departments).Returns(...) won't work unless the property is virtual.
            mockContext.Object.Departments = mockSet.Object;

            // SaveChangesAsync should not be called in this case; but set up a default if invoked
            mockContext
                .Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            var repository = new DepartmentRepository(mockContext.Object);

            // Act
            var result = await repository.DeleteDepartment(99);

            // Assert
            Assert.Equal("Not Found", result);

            // Verify FindAsync called with id 99
            mockSet.Verify(m => m.FindAsync(It.Is<object[]>(o => (int)o[0] == 99)), Times.Once);

            // Verify Remove and SaveChangesAsync were never called
            mockSet.Verify(m => m.Remove(It.IsAny<DepartmentEntity>()), Times.Never);
            mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }


    }
}
