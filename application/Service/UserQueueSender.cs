using Amazon.SQS;
using Amazon.SQS.Model;
using API_AUTENTICATION.domain.Interfaces.Service;
using authentication_API.domain.entities;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace API_AUTENTICATION.application.Service
{
    public class UserQueueSender : IUserQueueSender
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly string _queueUrl;

        public UserQueueSender(
            IAmazonSQS sqsClient, 
            IConfiguration config) {

            _sqsClient = sqsClient;
            _queueUrl = config["AWS:SQSQueueUrl"];
        }
        public async Task SendUserToQueueAsync(User user)
        {
            var menssageBody = JsonSerializer.Serialize(user);

            var request = new SendMessageRequest {
                QueueUrl = _queueUrl,
                MessageBody = menssageBody
            };

            await _sqsClient.SendMessageAsync(request);

        }
    }
}
