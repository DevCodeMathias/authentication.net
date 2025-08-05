namespace API_AUTENTICATION.domain.Contract
{
    public class UserEnvelopeDto
    {
        public Guid MessageId { get; set; }
        public DateTime TimesTamps { get; set; }
        public string EventType { get; set; }
        public string source { get; set; }
        public UserPayloadDto Payload { get; set; }
    }
}
