using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GetOrderDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderTime { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactNumber { get; set; }
        public string? Note { get; set; }
        public string Currency { get; set; }
        public decimal TotalAmount { get; set; }

        public int PaymentMethodId { get; set; }
        public int OrderStatusId { get; set; }

        public GetOrderStatusDTO OrderStatus { get; set; }
        public GetPaymentMethodDTO PaymentMethod { get; set; }
        public List<GetOrderItemDTO> Items { get; set; } = new List<GetOrderItemDTO>();
    }
}
