using AutoMapper;
using Store.Domain.Contracts;
using Store.Domain.Entities.Orders;
using Store.Domain.Entities.Products;
using Store.Domain.Exceptions;
using Store.Services.Abstraction.Orders;
using Store.Services.Specifications.Orders;
using Store.Shard.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Orders
{
    public class OrderService (IUniteOfWork _uniteOfWork,IMapper _mapper,IBasketRepository _basketRepository): IOrderService
    {
        public async Task<OrderResponse> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            // 1. Get OrderAddress

           var orderAddress= _mapper.Map<OrderAddress>(request.ShipToAddress);

            // 2.Get Delivery method By Id 

           var deliveryMethod=await _uniteOfWork.GetRepository<int, DeliveryMethode>().GetAsync(request.DeliveryMethodId);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);

            // 3. Get order items
            // 3.1 Get Basket By Id 

            var basket = await _basketRepository.GetBasketAsync(request.BasketId);
            if (basket is null) throw new BasketNotFoundException(request.BasketId);

            //3.2  Convert Every Basket item to OrderItem 

            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items) {
                // check price in data base 
                // Get Product from database 
               var product = await _uniteOfWork.GetRepository<int, Product>().GetAsync(item.Id);
                if (product is null) throw new ProductNotFoundExceptions(item.Id);

                if(product.Price!= item.Price) item.Price = product.Price;

                var productInOrderItem = new ProductInOrderItem(item.Id,item.ProductName,item.PicturalUrl);
                var orderItem  =    new OrderItem(productInOrderItem, item.Price,item.Quantity);
                orderItems.Add(orderItem);
            }

            // 4. Calculate SubTotal
            var subTotal = orderItems.Sum(OI=>OI.Price*OI.Quantity);

            //Create Order 
            var order = new Order(userEmail, orderAddress, deliveryMethod, orderItems, subTotal);
            
            // Add order in database 
           await _uniteOfWork.GetRepository<Guid, Order>().AddAsync(order);
           var count =await _uniteOfWork.SaveChangesAsync();
            if (count <= 0) throw new CreateOrderBadRequestException();
            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<DeliveryMethodsResponse>> GetAllDeliveryMethodsAsync()
        {
          var deliveryMethods=await  _uniteOfWork.GetRepository<int, DeliveryMethode>().GetAllAsync();

            return _mapper.Map<IEnumerable<DeliveryMethodsResponse>>(deliveryMethods);
        }

        public async Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string userEmail)
        {
            var spec = new OrderSpecification(id, userEmail);
            var order = await _uniteOfWork.GetRepository<Guid, Order>().GetAsync(spec);
            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<OrderResponse>> GetOrderForSpecificUserAsync(string userEmail)
        {
            var spec = new OrderSpecification(userEmail);
            var order = await _uniteOfWork.GetRepository<Guid, Order>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<OrderResponse>>(order);
        }
    }
}
