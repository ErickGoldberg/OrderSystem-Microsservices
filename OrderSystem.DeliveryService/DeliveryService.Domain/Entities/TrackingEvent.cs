namespace DeliveryService_Domain.Entities
{
    public class TrackingEvent(string description)
    {
        public Guid TrackingEventId { get; private set; } = Guid.NewGuid();
        public string Description { get; private set; } = description;
        public DateTime EventDate { get; private set; } = DateTime.Now;
    }

}
