using API_AUTENTICATION.domain.entities;
using authentication_API.domain.entities;

namespace API_AUTENTICATION.domain.Interfaces.Service
{
    public interface IUserQueueSender
    {
        Task SendUserToQueueAsync(MessageEnvelope<User> user);
    }
}
