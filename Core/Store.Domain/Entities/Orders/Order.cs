using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Orders
{
    public  class Order : BaseEntity <Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail, OrderAddress shippingAddress, DeliveryMethode deliveryMethode, ICollection<OrderItem> items, decimal subTotal)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethode =    deliveryMethode;
            Items = items;
            SubTotal = subTotal;
        }

        public string UserEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public OrderAddress ShippingAddress { get; set; }
        public DeliveryMethode DeliveryMethode { get; set; } // Navigational Property   
        public int DeliveryMethodeId { get; set; } // FK
        public ICollection<OrderItem> Items { get; set; } // Navigational Property  
        public decimal SubTotal { get; set; }//  Price * Quantity
        //[NotMapped ]
        //public decimal Total { get; set; } // SubTotal + DeliveryMethod Cost

        public decimal GetTotal() => SubTotal + DeliveryMethode.Price; //Not Mapped 

    }
}
