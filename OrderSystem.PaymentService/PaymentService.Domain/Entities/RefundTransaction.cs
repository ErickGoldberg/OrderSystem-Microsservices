using Utilities.Domain;

namespace OrderSystem.PaymentService.Domain.Entities
{
    public class RefundTransaction(Guid paymentTransactionId, decimal refundAmount) : BaseEntity
    {
        public Guid PaymentTransactionId { get; private set; } = paymentTransactionId;
        public decimal RefundAmount { get; private set; } = refundAmount;
        public DateTime RefundDate { get; private set; } = DateTime.Now;

        public void ProcessRefund()
        {
            // TODO
            SetUpdatedAtDate(DateTime.Now);
        }
    }
}
