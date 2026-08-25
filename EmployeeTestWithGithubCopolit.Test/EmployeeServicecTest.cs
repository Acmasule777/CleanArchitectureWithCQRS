using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyApI.API.Controllers;
using MyAPI.Application.Commands.EmployeeCommand;
using MyAPI.Application.Interfaces;
using MyAPI.Application.Validation;
using MyAPI.Core.DTO;

namespace EmployeeTestWithGithubCopolit.Test
{
    public class EmployeeServicecTest
    {



        //Write The Unit test for the create Employee Validation
        //test createEmployeeCommandValidation rule when employee is create

        private readonly createEmployeeCommandValidations _validationRules = new();

        //check validation for this employee service incomingdata is valid or not

        [Theory]
        [InlineData("", "pune","", "Engineering")]
        [InlineData((string)null, "pune","", "Engineering")]
        [InlineData("Jayeshpunekmjnbhnbhnjbhjbhnhbhnjbbijkjiutjgnhmvngjnvv", "pune","", "Engineering")]
        public void Name_WhenInvalid_ShouldHaveValidationError(string name, string city, string Email, string departmentName)
        {
            var command = new createEmployeeCommand(name, city,Email, departmentName);

            var result = _validationRules.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name");
        }


        //Write Unit test for delete employee and return the expected string with given id

        [Theory]
        [InlineData(9)]
        [InlineData(5)]

        public async Task DeleteEmployee_WhenCalled_ReturnsExpectedStringNew(int id)
        {
            //Arrange
            var mockData = new Mock<IEmployee>();

            mockData.Setup(m => m.DeleteEmployee(id)).ReturnsAsync("Employee Deleted Successfully");
            //Act
            var result = await mockData.Object.DeleteEmployee(id);

            //Assert
            Assert.Equal("Employee Deleted Successfully", result);

        }

        [Fact]
        public async Task UpdateEmployee_ReturnsExpectedString()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var expected = "Employee Updated Successfully";

            var dto = new EmployeeUpdateDto
            {
                Id = 1,
                Name = "Updated Name",
                City = "Updated City"
            };

            // Setup mediator to return expected string when an updateEmployeeCommand is sent
            mockMediator
                .Setup(m => m.Send(It.IsAny<updateEmployeeCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var controller = new EmployeeController(mockMediator.Object);

            // Act
            var actionResult = await controller.UpdateEmployee(dto);

            // Assert: actionResult is OkObjectResult and contains expected string
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(expected, okResult.Value);

            // Verify mediator.Send was invoked once
            mockMediator.Verify(m => m.Send(It.IsAny<updateEmployeeCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }





}

