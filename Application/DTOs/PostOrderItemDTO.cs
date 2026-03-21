using Domain.Models;

namespace Application.DTOs
{
    public class PostOrderItemDTO
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public OrderItem ToModel() 
        {
            return new OrderItem
            { 
                Name = Name,
                UnitPrice = UnitPrice,
                Quantity = Quantity 
            };
        }
    }
}
