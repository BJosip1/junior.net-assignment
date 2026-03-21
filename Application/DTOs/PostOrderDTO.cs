using Domain.Models;

namespace Application.DTOs
{
    public class PostOrderDTO
    {
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactNumber { get; set; }
        public string? Note { get; set; }
        public string Currency { get; set; }
        public int PaymentMethodId { get; set; }
        public List<PostOrderItemDTO> Items { get; set; } = new List<PostOrderItemDTO>();

        public Order ToModel()
        {

            return new Order
            {
                CustomerName = CustomerName,
                DeliveryAddress = DeliveryAddress,
                ContactNumber = ContactNumber,
                Note = Note,
                Currency = Currency,
                PaymentMethodId = PaymentMethodId,
                Items = Items.Select(i => i.ToModel()).ToList()
            };
        }
    }
}
