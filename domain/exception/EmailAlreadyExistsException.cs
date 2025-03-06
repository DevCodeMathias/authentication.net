namespace API_AUTENTICATION.domain.exception
{
    public class EmailAlreadyExistsException : BusinessException
    {
        public EmailAlreadyExistsException() : base("Email already exists.") { }
    }
}
