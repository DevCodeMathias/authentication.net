using API_AUTENTICATION.domain.entities;
using API_AUTENTICATION.domain.Interfaces.Service;
using authentication_API.domain.entities;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;



public class RabbitMqUserPublisher : IUserQueueSender
{
    private readonly IConnection _connection;   
    private readonly string _queueName;
    private readonly string _deadLetterQueueName;

    public RabbitMqUserPublisher(IConnection channel, IConfiguration configuration)
    {
        _connection = channel;
        _queueName = configuration["RabbitMQ:QueueName"];
        _deadLetterQueueName = configuration["RabbitMQ:DlqQueueName"];
                    
    }

    public  Task SendUserToQueueAsync(MessageEnvelope<User> user)
    {
        using var channel = _connection.CreateModel();
        
        
        var mainQueuArgs = new Dictionary<string, object>
        {
            { "x-dead-letter-exchange", _deadLetterQueueName + ".exchange" },
            { "x-dead-letter-routing-key", _deadLetterQueueName },
        };
        
        
        channel.ExchangeDeclare(
            exchange:_deadLetterQueueName + ".exchange",
            type: ExchangeType.Direct,
            durable: true,
            autoDelete: false
            );
        
        channel.QueueDeclare(queue: _deadLetterQueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        channel.QueueDeclare(queue: _queueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: mainQueuArgs);
        
        channel.QueueBind(
            queue: _deadLetterQueueName,
            exchange: _deadLetterQueueName + ".exchange",
            routingKey: _deadLetterQueueName
        );
        var message = JsonConvert.SerializeObject(user);
        var body = Encoding.UTF8.GetBytes(message);

       channel.BasicPublish(exchange: "",
                             routingKey: _queueName,
                             basicProperties: null,
                             body: body);

        return Task.CompletedTask;
    }
}







