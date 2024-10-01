using OrderSystem.OrderService.Domain.ValueObjects;
using OrderSystem.PaymentService.Domain.Enums;

namespace OrderSystem.PaymentService.Domain.Entities
{
    public class Transaction
    {
        public Guid TransactionId { get; private set; }
        public Money Amount { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public PaymentStatus Status { get; private set; }
        public string GatewayResponse { get; private set; }

        public Transaction(Money amount)
        {
            Amount = amount;

            TransactionId = Guid.NewGuid();
            TransactionDate = DateTime.Now;
            Status = PaymentStatus.Pending;
        }

        public void MarkAsSuccessful(string response)
        {
            Status = PaymentStatus.Success;
            GatewayResponse = response;
        }

        public void MarkAsFailed(string response)
        {
            Status = PaymentStatus.Failed;
            GatewayResponse = response;
        }
    }
}
