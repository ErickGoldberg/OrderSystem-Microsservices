namespace Utilities.Domain.ValueObjects
{
    public class Address(string street, string city, string state, string postalCode)
    {
        public string Street { get; private set; } = street;
        public string City { get; private set; } = city;
        public string State { get; private set; } = state;
        public string PostalCode { get; private set; } = postalCode;
    }

}
