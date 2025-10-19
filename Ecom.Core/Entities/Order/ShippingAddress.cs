namespace Ecom.Core.Entities.Order
{
    public class ShippingAddress : BaseEntity<int>
    {
        public ShippingAddress()
        {
            
        }
        public ShippingAddress(string? firstName, string? lastName, string? zipCode, string? city, string? street, string? state)
        {
            FirstName = firstName;
            LastName = lastName;
            ZipCode = zipCode;
            City = city;
            Street = street;
            State = state;
        }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? State { get; set; }
    }
}