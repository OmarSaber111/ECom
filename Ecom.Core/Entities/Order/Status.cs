namespace Ecom.Core.Entities.Order
{
    public enum Status
    {
        Pending,
        PaymentReceived,
        PaymentFailed,
        Shipped,
        Delivered,
        Cancelled,
        Refunded
    }
}