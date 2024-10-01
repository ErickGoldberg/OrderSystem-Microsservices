using DeliveryService_Domain.Enums;
using Utilities.Domain;

namespace DeliveryService_Domain.Entities
{
    public class DeliveryPerson(string name, string phoneNumber) : BaseEntity
    {
        public string Name { get; private set; } = name;
        public string PhoneNumber { get; private set; } = phoneNumber;
        public List<Delivery> Deliveries { get; private set; } = new();

        public void AssignDelivery(Delivery delivery)
        {
            if (delivery.Status != DeliveryStatus.Pending)
                throw new InvalidOperationException("Delivery must be pending to be assigned.");

            Deliveries.Add(delivery);
            delivery.StartDelivery();
        }
    }

}
