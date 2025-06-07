namespace API_AUTENTICATION.domain.Interfaces.Service
{
    public interface IAuthenticationService
    {
        Task<bool> login(string email, string password);

    }
}
