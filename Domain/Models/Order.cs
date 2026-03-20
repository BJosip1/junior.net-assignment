using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderTime { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactNumber { get; set; }
        public string? Note { get; set; }
        public string Currency { get; set; }
        public int PaymentMethodId { get; set; }
        public int OrderStatusId { get; set; }

        #region Navigation Properties
        public OrderStatus OrderStatus { get; set; } 
        public PaymentMethod PaymentMethod { get; set; } 
        #endregion

        #region Reverse Navigation Properties
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        #endregion
    }
}
