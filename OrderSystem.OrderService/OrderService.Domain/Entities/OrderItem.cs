using OrderSystem.OrderService.Domain.ValueObjects;

namespace OrderSystem.OrderService.Domain.Entities
{
    public class OrderItem
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public Money UnitPrice { get; private set; }
        public Money TotalPrice => UnitPrice.Multiply(Quantity);

        public OrderItem(Guid productId, int quantity, Money unitPrice)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            Quantity = newQuantity;
        }
    }

}
