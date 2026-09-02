using MediatR;
using MyAPI.Application.Interfaces;
using RabbitMQ.Client;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyAPI.Infrastructure.messanger
{
    public class RabbitMQPublisherDepartmentName: IPublishDepartmentName
    {
        public async Task PublishMessageForDepartment(GetDepartmentRequest request)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq-docker",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            var connection = await factory.CreateConnectionAsync();

            var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                    exchange: "departmentName.exchange",
                    type: ExchangeType.Direct,
                    durable: true,
                    autoDelete: false);

            var json = JsonSerializer.Serialize(request);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(
                exchange: "departmentName.exchange",
                routingKey: "departmentRequestKey",
                body: body);
        }

    }
}
