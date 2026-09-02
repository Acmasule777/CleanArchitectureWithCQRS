using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nofication.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;
using System.Text;
using System.Text.Json;

namespace Notification.Infrastructure.Messaging;

public class EmployeeCreatedConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public EmployeeCreatedConsumer(
        IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellation)
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
                // Convert RabbitMQ message to string
                var json = Encoding.UTF8.GetString(args.Body.ToArray());

                Console.WriteLine($"Message received: {json}");

                // Convert JSON to EmployeeCreatedMessage
                var employee = JsonSerializer.Deserialize<EmployeeCreatedMessage>(json);

                if (employee == null)
                    return;

                Console.WriteLine( $"Employee Name: {employee.EmployeeName}");

                Console.WriteLine($"Employee Email: {employee.EmployeeEmail}");

                // Get EmailService
                using var scope = _scopeFactory.CreateScope();

                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                // Email content
                var emailBody = $"""
                    Hello {employee.EmployeeName},

                    Welcome to our organization!

                    Your employee account has been created successfully.

                    Employee Email: {employee.EmployeeEmail}

                    Regards,
                    HR Team
                    """;

                // Send email
                await emailService.SendEmail(employee.EmployeeEmail,emailBody);

                Console.WriteLine("Email sent successfully.");

                // Message processed successfully
                await channel.BasicAckAsync(args.DeliveryTag,false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing message: {ex.Message}");

                // Put message back into queue
                await channel.BasicNackAsync(args.DeliveryTag,false,true);
            }
        };

        // Consume from EXISTING queue
        await channel.BasicConsumeAsync(queue: "EmployeeCreate", autoAck: false,consumer: consumer);

        Console.WriteLine("Waiting for EmployeeCreated messages...");

        await Task.Delay(Timeout.Infinite);
    }
}