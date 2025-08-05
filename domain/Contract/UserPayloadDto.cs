namespace API_AUTENTICATION.domain.Contract
{
    public class UserPayloadDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string IsVerified { get; set; } 
    }
}
