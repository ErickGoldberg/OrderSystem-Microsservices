namespace OrderSystem.PaymentService.Domain.Entities
{
    public class PaymentMethod(string methodName)
    {
        public Guid PaymentMethodId { get; private set; } = Guid.NewGuid();
        public string MethodName { get; private set; } = methodName; // Ex: CreditCard, Boleto, PayPal
    }

}
