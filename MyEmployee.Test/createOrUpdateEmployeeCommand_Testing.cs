using Moq;
using MyAPI.Application.Commands.EmployeeCommand;
using MyAPI.Application.Interfaces;
using MyAPI.Application.Validation;
using MyAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEmployee.Test
{
    public class createOrUpdateEmployeeCommand_Testing
    {
        private readonly createEmployeeCommandValidations _validations = new();

        [Theory]
        [InlineData("", "pune","", "Engineering")]
        [InlineData((string)null, "pune","", "Engineering")]
        [InlineData("Jayeshpunekmjnbhnbhnjbhjbhnhbhnjbbijkjiutjgnhmvngjnvv", "pune", "","Engineering")]

        public void HandleValidationWhenCreateEmployeeforEmployeeName(string name, string city,string email, string departmentname)
        {
            var command = new createEmployeeCommand(name, city, email, departmentname);

            var result = _validations.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name");

        }

        [Theory]
        [InlineData("jayesh", "","", "Engineering")]
        [InlineData("Jayesh", (string)null,"", "Engineering")]
        [InlineData("Jayesh", "punekmjnbhnbhnjbhjbhnhbhnjbbijkj","", "Engineering")]

        public void HandleValidationWhenCreateEmployeeForCity(string name, string city,string email, string departmentname)
        {
            var command = new createEmployeeCommand(name, city,email, departmentname);

            var result = _validations.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "City");
        }


        [Theory]
        [InlineData("Atul", "Dhule","", "Engineering", "Employee Successfully Created")]

        public async Task HandleTestWhenSuccessfullyCreateEmployee(string name, string city,string email, string department, string expectedString)
        {
            var mockData = new Mock<IEmployee>();
            var mockDepartmentClient = new Mock<IDepartmentServiceClient>();

            mockData.Setup(e => e.AddEmployee(It.Is<EmployeeDto>(e => e.Name == name && e.City == city)))
            .ReturnsAsync(expectedString);

            mockDepartmentClient
                    .Setup(x => x.GetDepartmentByNameAsync(department))
                    .ReturnsAsync(new DepartmentDto { DepartmentId = 2, DepartmentName = department });

            var command = new createEmployeeCommand(name, city, email, department);
            var handler = new createCommandHandler(mockData.Object, mockDepartmentClient.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal(expectedString, result);

        }

    }
}
