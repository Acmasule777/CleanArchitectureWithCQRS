using Department.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Department.Infrastructure.Messaging
{
    public class DepartmentConsumerFromEmployee : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DepartmentConsumerFromEmployee(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq-docker",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            var connection = await factory.CreateConnectionAsync();

            var channel = await connection.CreateChannelAsync();

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (sender, args) =>
            {
                try
                {
                    var json = Encoding.UTF8.GetString(args.Body.ToArray());
                    var request = JsonSerializer.Deserialize<GetDepartmentRequest>(json);

                    if (request == null)
                        return;

                    var scope = _scopeFactory.CreateScope();
                    var GetIdByName = scope.ServiceProvider.GetRequiredService<IDepartment>();

                    await GetIdByName.GetByNameAsync2(request);

                    await channel.BasicAckAsync(args.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");

                    // Put message back into queue
                    await channel.BasicNackAsync(args.DeliveryTag, false, true);
                }
            };

            await channel.BasicConsumeAsync(queue: "DepartmentNameRequest.Queue", autoAck: false, consumer: consumer);

            Console.WriteLine("Waiting for EmployeeCreated messages...");

            await Task.Delay(Timeout.Infinite);
        }
    }
}
