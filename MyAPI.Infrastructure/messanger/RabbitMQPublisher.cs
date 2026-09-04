using System.Text;
using System.Text.Json;
using MyAPI.Application.Interfaces;
using RabbitMQ.Client;
using Shared;

namespace Employee.Infrastructure.Messaging;

public class RabbitMQPublisher : IRabbitMQPublisher
{
    public async Task PublishEmployeeCreatedAsync(
        EmployeeCreatedMessage message)
    {
        //var factory = new ConnectionFactory
        //{
        //    HostName = "rabbitmq-docker",
        //    Port = 5672,
        //    UserName = "guest",
        //    Password = "guest"
        //};

        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5673,
            UserName = "guest",
            Password = "guest"
        };

        var connection =
            await factory.CreateConnectionAsync();

         var channel =
            await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(
            exchange: "employee.exchange",
            type: ExchangeType.Direct,
            durable: true,
            autoDelete: false);

        var json = JsonSerializer.Serialize(message);

        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(
            exchange: "employee.exchange",
            routingKey: "employee.created",
            body: body);
    }
}