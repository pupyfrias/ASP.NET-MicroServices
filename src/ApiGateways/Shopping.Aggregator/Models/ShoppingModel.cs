namespace Shopping.Aggregator.Models
{
    public class ShoppingModel
    {
        public required string UserName { get; set; }
        public required BasketModel BasketWithProducts { get; set; }
        public required IEnumerable<OrderResponseModel> Orders { get; set; }
    }
}
