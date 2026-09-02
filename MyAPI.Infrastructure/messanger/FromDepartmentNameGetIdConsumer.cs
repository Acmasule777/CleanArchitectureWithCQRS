using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyAPI.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyAPI.Infrastructure.messanger
{
    public class FromDepartmentNameGetIdConsumer : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public FromDepartmentNameGetIdConsumer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5673,
                UserName = "guest",
                Password = "guest"
            };

            var connection = await factory.CreateConnectionAsync();

            var channel = await connection.CreateChannelAsync();

            // Consume from your existing queue
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, args) =>
            {
                try
                {
                    var json = Encoding.UTF8.GetString(args.Body.ToArray());
                    var body = JsonSerializer.Deserialize<GetDepartmentResponse>(json);

                    if (body == null)
                        return;

                    using var scope = _scopeFactory.CreateScope();
                    var GetResponseFromDepartment = scope.ServiceProvider.GetRequiredService<IDepartmentIdResponseService>();

                    GetResponseFromDepartment.SetResponse(body);
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Error processing message: {ex.Message}");

                    // Put message back into queue
                    await channel.BasicNackAsync(args.DeliveryTag, false, true);
                }
            };

            await channel.BasicConsumeAsync(queue: "DepartmentResponse.Queue", autoAck: false, consumer: consumer);

            Console.WriteLine("Waiting for EmployeeCreated messages...");

            await Task.Delay(Timeout.Infinite);

        }
    }
}
