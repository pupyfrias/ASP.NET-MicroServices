namespace Basket.API.Entities.V1
{
    public class ShoppingCarItem
    {
        public int Quantity { get; set; }
        public required string Color { get; set; }
        public required decimal Price { get; set; }
        public required string ProductId { get; set; }
        public required string ProductName { get; set; }

    }
}