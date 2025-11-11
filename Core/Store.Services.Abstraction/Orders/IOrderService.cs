using Store.Shard.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Abstraction.Orders
{
    public interface IOrderService
    {
      Task<OrderResponse>  CreateOrderAsync(OrderRequest request,string userEmail  );
      Task<IEnumerable<DeliveryMethodsResponse>> GetAllDeliveryMethodsAsync();
       Task<OrderResponse?>  GetOrderByIdForSpecificUserAsync(Guid id,string userEmail);
       Task<IEnumerable<OrderResponse>> GetOrderForSpecificUserAsync(string userEmail);
    }
}
