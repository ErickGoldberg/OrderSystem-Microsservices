namespace DeliveryService_Domain.ValueObjects
{
    public class DeliveryWindow
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public DeliveryWindow(DateTime start, DateTime end)
        {
            if (start >= end)
                throw new ArgumentException("Start time must be earlier than end time.");

            Start = start;
            End = end;
        }
    }

}
