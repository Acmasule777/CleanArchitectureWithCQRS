using Department.Application.Interfaces;
using RabbitMQ.Client;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Department.Infrastructure.Messaging
{
    public class DepartmentIdMessagePublisher : IDepartmentIdPublisher
    {
        public async Task DepartmentIdPublishByName(GetDepartmentResponse responce)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5673,
                UserName = "guest",
                Password = "guest",
            };

            var connection = await factory.CreateConnectionAsync();

            var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: "DepartmentResponsePublish.exchange",
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false);

            var json = JsonSerializer.Serialize(responce);

            var body = Encoding.UTF8.GetBytes(json);

           await channel.BasicPublishAsync(
                exchange: "DepartmentResponsePublish.exchange",
                routingKey: "DepartmentResponseKey",
                body: body);

        }
    }
}
