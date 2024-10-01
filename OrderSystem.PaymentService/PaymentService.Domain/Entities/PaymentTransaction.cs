using OrderSystem.PaymentService.Domain.Enums;
using Utilities.Domain;

namespace OrderSystem.PaymentService.Domain.Entities
{
    public class PaymentTransaction(Guid orderId, decimal amount, PaymentMethodEnum paymentMethod)
        : BaseEntity
    {
        public Guid OrderId { get; private set; } = orderId;
        public decimal Amount { get; private set; } = amount;
        public PaymentMethodEnum PaymentMethod { get; private set; } = paymentMethod;
        public DateTime PaymentDate { get; private set; } = DateTime.Now;

        public void UpdateAmount(decimal newAmount)
        {
            Amount = newAmount;
            SetUpdatedAtDate(DateTime.Now);
        }

        public void ChangePaymentMethod(PaymentMethodEnum newPaymentMethod)
        {
            PaymentMethod = newPaymentMethod;
            SetUpdatedAtDate(DateTime.Now);
        }
    }
}
