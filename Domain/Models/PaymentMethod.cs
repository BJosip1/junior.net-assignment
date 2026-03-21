using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }

        #region Reverse Navigation Properties
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        #endregion
    }
}
