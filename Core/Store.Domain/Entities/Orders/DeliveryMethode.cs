using Store.Domain.Entities.Products;

namespace Store.Domain.Entities.Orders
{
    // Table
    public class DeliveryMethode:BaseEntity<int>
    {
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Price  { get; set; }
        
    }
}