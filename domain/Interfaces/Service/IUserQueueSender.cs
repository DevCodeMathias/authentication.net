using authentication_API.domain.entities;

namespace API_AUTENTICATION.domain.Interfaces.Service
{
    public interface IUserQueueSender
    {
        Task SendUserToQueueAsync(User user);
    }
}
