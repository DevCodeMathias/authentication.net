namespace authentication_API.domain.entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsVerified { get; set; }

    }
}
