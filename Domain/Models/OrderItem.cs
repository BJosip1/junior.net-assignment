using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        #region Navigation Properties
        public Order Order { get; set; }
        #endregion
    }
}
