using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        #region Reverse Navigation Properties
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        #endregion
    }
}
