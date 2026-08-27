using MediatR;
using MyAPI.Application.Interfaces;
using MyAPI.Core.DTO;
using Shared;


namespace MyAPI.Application.Commands.EmployeeCommand
{
    public record createEmployeeCommand (string Name, string City, string Email, string DepartmentName) : IRequest<string>;
    public class createCommandHandler(IEmployee Emprepository, 
        IDepartmentServiceClient DmpRepository, 
        IRabbitMQPublisher rabbitMQPublisher
        /*IPublishDepartmentName rabbitmqDepartmentName,
        IDepartmentIdResponseService ResposeFromDepartment*/) : IRequestHandler<createEmployeeCommand, string>
    {

        public async Task<string> Handle(createEmployeeCommand request, CancellationToken cancellationToken)
        {

            //this is object for request-reply processing
            //var departmentNameRequest = new GetDepartmentRequest
            //{
            //    DepartmentName = request.DepartmentName,
            //    CorrelationId = Guid.NewGuid().ToString()
            //};

           var existingDepartment = await DmpRepository.GetDepartmentByNameAsync(request.DepartmentName);

            //This below code is for RabbitMq Request-reply messaging
           // await rabbitmqDepartmentName.PublishMessageForDepartment(departmentNameRequest);

           //var existedDepartment = await ResposeFromDepartment.returnToHandler();

            int departmentId;

            if (existingDepartment is not null)
            {
                // Found it — reuse the existing Id
                departmentId = existingDepartment.DepartmentId;
            }
            else
            {
                // Not found — create it, get back the new Id
                departmentId = await DmpRepository.CreateDepartmentAsync(request.DepartmentName);
            }

            // Step 2: save the employee with the resolved DepartmentId (never the name)
            var employee = new EmployeeDto
            {
                Name = request.Name,
                City = request.City,
                Email = request.Email,
                DepartmentId = departmentId
            };

            var empId = await Emprepository.AddEmployee(employee);

            var message = new EmployeeCreatedMessage
            {
                EmployeeId = empId,
                EmployeeName = employee.Name,
                EmployeeEmail = employee.Email
            };

            await rabbitMQPublisher.PublishEmployeeCreatedAsync(message);

            return "Employee Successfully Created";
        }
    }

}
