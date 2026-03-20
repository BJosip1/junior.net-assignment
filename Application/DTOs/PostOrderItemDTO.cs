using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
