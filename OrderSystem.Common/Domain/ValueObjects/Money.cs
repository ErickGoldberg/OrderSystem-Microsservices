namespace OrderSystem.OrderService.Domain.ValueObjects
{
    public class Money : IEquatable<Money>
    {
        public decimal Amount { get; }

        public Money(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative.");

            Amount = amount;
        }

        public Money Add(Money other) => new Money(Amount + other.Amount);
        public Money Subtract(Money other) => new Money(Amount - other.Amount);
        public Money Multiply(int factor) => new Money(Amount * factor);

        public bool IsGreaterThan(Money other) => Amount > other.Amount;
        public bool IsLessThan(Money other) => Amount < other.Amount;

        public override bool Equals(object? obj) => Equals(obj as Money);
        public bool Equals(Money? other) => other != null && Amount == other.Amount;
        public override int GetHashCode() => Amount.GetHashCode();
    }
}
