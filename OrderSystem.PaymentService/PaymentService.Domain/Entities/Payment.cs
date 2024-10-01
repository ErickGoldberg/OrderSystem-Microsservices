using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderSystem.OrderService.Domain.ValueObjects;
using OrderSystem.PaymentService.Domain.Enums;
using Utilities.Domain;

namespace OrderSystem.PaymentService.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid OrderId { get; private set; }
        public Money Amount { get; private set; }
        public PaymentMethod Method { get; private set; }
        public PaymentStatus Status { get; private set; }
        private List<Transaction> _transactions;
        public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

        public Payment(Guid orderId, Money amount, PaymentMethod method)
        {
            OrderId = orderId;
            Amount = amount;
            Method = method;
            Status = PaymentStatus.Pending;
            _transactions = new List<Transaction>();
        }

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);

            Status = transaction.Status switch
            {
                PaymentStatus.Success => PaymentStatus.Completed,
                PaymentStatus.Failed => PaymentStatus.Failed,
                _ => Status
            };
        }
    }

}
