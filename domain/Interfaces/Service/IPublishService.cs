using API_AUTENTICATION.domain.entities;
using authentication_API.domain.entities;

namespace API_AUTENTICATION.domain.Interfaces.Service
{
    public interface IPublishService
    {

       Task PublishToTopicAsync(MessageEnvelope<User> UserEnvelope);
    }
}
