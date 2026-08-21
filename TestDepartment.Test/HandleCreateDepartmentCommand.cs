using Department.Application.Commands.Department;
using Department.Application.Interfaces;
using Department.Application.Validation;
using DepartmentCore.Core.DTOs;
using Moq;


namespace TestDepartment.Test
{
    public class HandleCreateDepartmentCommand
    {

        private readonly CreateDepartmentValidationCommand _validate = new();

        [Theory]
        [InlineData("Engineering", "Department is already exists")]
        [InlineData("Sales", "Department Successfully Created")]


        public async Task Handle_createDepartmentCommandDepartmentExists(string departmentname, string expectedResult)
        {
            //Arrange

            Mock<IDepartment> mockData = new Mock<IDepartment>();

            mockData.Setup(d => d.CreateDepartment(It.Is<DepartmentDto>(dto => dto.DepartmentName == departmentname)))
                    .ReturnsAsync(expectedResult);

            var command = new createDepartmentCommand(new DepartmentDto { DepartmentName = departmentname });
            var handler = new createDepartmentHandler(mockData.Object);

            //Act

            var result = await handler.Handle(command, CancellationToken.None);

            //Asserts

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData((string)null)]
        public void Handle_ValidationIfDepartmentNameIsNotValid(string departmentInput)
        {
            var command = new createDepartmentCommand(new DepartmentDto
            {
                DepartmentId = 1,
                DepartmentName = departmentInput
            });

            var result = _validate.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Dto.DepartmentName");
        }

    }
}
