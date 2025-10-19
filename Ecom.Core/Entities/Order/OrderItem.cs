namespace Ecom.Core.Entities.Order
{
    public class OrderItem : BaseEntity<int>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(int productId, string? productName, string? mainImg, decimal price, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            MainImg = mainImg;
            Price = price;
            Quantity = quantity;
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? MainImg { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}