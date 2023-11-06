namespace Basket.API.Entities.V1
{
    public class ShoppingCar
    {
        public string UserName { get; set; }
        public IEnumerable<ShoppingCarItem> Items { get; set; } = new List<ShoppingCarItem>();
        public decimal TotalPrice
        {
            get
            {
                return Items.Select(item => item.Price * item.Quantity).Sum();
            }
        }

        public ShoppingCar(string userName)
        {
            UserName = userName;
        }

    }
}
