using MediatR;
using MyAPI.Application.Interfaces;
using MyAPI.Core.DTO;
using Shared;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;


namespace MyAPI.Application.Commands.EmployeeCommand
{
    public record createEmployeeCommand (string Name, string City, string Email, string DepartmentName, string IdempotencyKey) : IRequest<string>;
    public class createCommandHandler(IEmployee Emprepository,
        IDepartmentServiceClient DmpRepository,
        IRabbitMQPublisher rabbitMQPublisher,
        IIdempotency potency
        /*IPublishDepartmentName rabbitmqDepartmentName,
        IDepartmentIdResponseService ResposeFromDepartment*/) : IRequestHandler<createEmployeeCommand, string>
    {

        public async Task<string> Handle(createEmployeeCommand request, CancellationToken cancellationToken)
        {
            var requestHash = CreateRequestHash(request);

            var existingRecord = await potency.GetAsync(request.IdempotencyKey, cancellationToken);

            if (existingRecord is not null)
            {

                if (existingRecord.RequestHash != requestHash)
                {
                    throw new InvalidOperationException(
                        "The idempotency key was used with a different request.");
                }

                if (existingRecord.Status == "Completed")
                {
                    return existingRecord.ResponseBody!;
                }

                //if (existingRecord.Status == IdempotencyStatus.Processing)
                //{
                //    throw new InvalidOperationException(
                //        "The request is already being processed.");
                //}
            }


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

            string status = "Completed";

            await potency.CreateAsync(
                request.IdempotencyKey,
                requestHash,
                  status,
                cancellationToken);

            var message = new EmployeeCreatedMessage
            {
                EmployeeId = empId,
                EmployeeName = employee.Name,
                EmployeeEmail = employee.Email
            };

            await rabbitMQPublisher.PublishEmployeeCreatedAsync(message);

            return "Employee Successfully Created";
        }


        private static string CreateRequestHash(createEmployeeCommand request)
        {
            var requestToHash = new
            {
                request.Name,
                request.City,
                request.Email,
                request.DepartmentName
            };

            var json = JsonSerializer.Serialize(requestToHash);

            var bytes = Encoding.UTF8.GetBytes(json);
            var hash = SHA256.HashData(bytes);

            return Convert.ToHexString(hash);
        }
    }
}
