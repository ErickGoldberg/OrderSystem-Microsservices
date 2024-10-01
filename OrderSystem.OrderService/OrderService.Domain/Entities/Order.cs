using OrderSystem.OrderService.Domain.Enums;
using OrderSystem.OrderService.Domain.ValueObjects;
using Utilities.Domain;

namespace OrderSystem.OrderService.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; private set; }
        public OrderStatusEnum OrderStatus { get; private set; }
        public DateTime OrderDate { get; private set; }
        private List<OrderItem> _items;
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        public Money TotalAmount { get; private set; }

        public Order(Guid customerId)
        {
            CustomerId = customerId;

            OrderStatus = OrderStatusEnum.Pending;
            OrderDate = DateTime.Now;
            _items = new List<OrderItem>();
            TotalAmount = new Money(0);
        }

        public void AddItem(OrderItem item)
        {
            if (OrderStatus != OrderStatusEnum.Pending)
                throw new InvalidOperationException("Cannot add items to an order that is not pending.");

            _items.Add(item);
            UpdateTotalAmount();
            SetUpdatedAtDate(DateTime.Now);
        }

        public void RemoveItem(OrderItem item)
        {
            if (OrderStatus != OrderStatusEnum.Pending)
                throw new InvalidOperationException("Cannot remove items from an order that is not pending.");

            _items.Remove(item);
            UpdateTotalAmount();
            SetUpdatedAtDate(DateTime.Now);
        }

        public void ChangeStatus(OrderStatusEnum newStatus)
        {
            if (OrderStatus is OrderStatusEnum.Completed or OrderStatusEnum.Cancelled)
                throw new InvalidOperationException("Cannot change the status of a completed or cancelled order.");

            if (OrderStatus == OrderStatusEnum.Pending && newStatus == OrderStatusEnum.Processing)
            {
                OrderStatus = newStatus;
                SetUpdatedAtDate(DateTime.Now);
            }
            else if (OrderStatus == OrderStatusEnum.Processing && (newStatus == OrderStatusEnum.Completed || newStatus == OrderStatusEnum.Cancelled))
            {
                OrderStatus = newStatus;
                SetUpdatedAtDate(DateTime.Now);
            }
            else
            {
                throw new InvalidOperationException($"Cannot change order status from {OrderStatus} to {newStatus}.");
            }
        }

        private void UpdateTotalAmount()
        {
            TotalAmount = _items.Aggregate(new Money(0), (sum, item) => sum.Add(item.TotalPrice));
        }

        public void CompleteOrder()
        {
            if (OrderStatus != OrderStatusEnum.Processing)
                throw new InvalidOperationException("Only processing orders can be completed.");

            OrderStatus = OrderStatusEnum.Completed;
            SetUpdatedAtDate(DateTime.Now);
        }

        public void CancelOrder()
        {
            if (OrderStatus != OrderStatusEnum.Processing && OrderStatus != OrderStatusEnum.Pending)
                throw new InvalidOperationException("Only pending or processing orders can be cancelled.");

            OrderStatus = OrderStatusEnum.Cancelled;
            SetUpdatedAtDate(DateTime.Now);
        }
    }
}
