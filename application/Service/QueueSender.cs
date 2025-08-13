using API_AUTENTICATION.domain.entities;
using API_AUTENTICATION.domain.Interfaces.Service;
using authentication_API.domain.entities;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;



public class QueueSender : IUserQueueSender
{
    private readonly IConnection _connection;   
    private readonly string _queueName;

    public QueueSender(IConnection channel, IConfiguration configuration)
    {
        _connection = channel;
        _queueName = configuration["RabbitMQ:QueueName"];
                    
    }

    public  Task SendUserToQueueAsync(MessageEnvelope<User> user)
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(queue: _queueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var message = JsonConvert.SerializeObject(user);
        var body = Encoding.UTF8.GetBytes(message);

       channel.BasicPublish(exchange: "",
                             routingKey: _queueName,
                             basicProperties: null,
                             body: body);

        return Task.CompletedTask;
    }
}







