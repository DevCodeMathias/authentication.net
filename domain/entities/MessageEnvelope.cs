namespace API_AUTENTICATION.domain.entities
{
    public class MessageEnvelope<T>
    {
        public Guid MessageId { get; set; }
        public DateTime TimesTamps { get; set; }
        public string EventType { get; set; }
        public string source { get; set; }
        public T Payload { get; set; }
    }
}
