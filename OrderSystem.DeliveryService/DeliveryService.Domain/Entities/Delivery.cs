using DeliveryService_Domain.Enums;
using DeliveryService_Domain.ValueObjects;
using Utilities.Domain;
using Utilities.Domain.ValueObjects;

namespace DeliveryService_Domain.Entities
{
    public class Delivery : BaseEntity
    {
        public Guid OrderId { get; private set; }
        public Address Destination { get; private set; }
        public DeliveryWindow Window { get; private set; }
        public DeliveryStatus Status { get; private set; }
        private List<TrackingEvent> _trackingEvents;
        public IReadOnlyCollection<TrackingEvent> TrackingEvents => _trackingEvents.AsReadOnly();

        public Delivery(Guid orderId, Address destination, DeliveryWindow window)
        {
            OrderId = orderId;
            Destination = destination;
            Window = window;
            Status = DeliveryStatus.Pending;
            _trackingEvents = new List<TrackingEvent>();
        }

        public void StartDelivery()
        {
            if (Status != DeliveryStatus.Pending)
                throw new InvalidOperationException("Delivery cannot be started in its current state.");

            Status = DeliveryStatus.InProgress;
            AddTrackingEvent("Delivery started.");
        }

        public void CompleteDelivery()
        {
            if (Status != DeliveryStatus.InProgress)
                throw new InvalidOperationException("Delivery must be in progress to be completed.");

            Status = DeliveryStatus.Completed;
            AddTrackingEvent("Delivery completed.");
        }

        public void FailDelivery(string reason)
        {
            if (Status == DeliveryStatus.Completed)
                throw new InvalidOperationException("Cannot fail a completed delivery.");

            Status = DeliveryStatus.Failed;
            AddTrackingEvent($"Delivery failed: {reason}");
        }

        private void AddTrackingEvent(string description)
        {
            _trackingEvents.Add(new TrackingEvent(description));
        }
    }

}
