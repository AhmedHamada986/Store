using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Store.Domain.Contracts;
using Store.Domain.Entities.Identity;
using Store.Services.Abstraction;
using Store.Services.Abstraction.Orders;
using Store.Services.Abstraction.Products;
using Store.Services.Orders;
using Store.Services.Products;
using Store.Shard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services
{   
    public class ServiceManger (IUniteOfWork _uniteOfWork, 
        IBasketRepository basketRepository,
        ICashRepository cashRepository,
        UserManager<AppUser> userManager,
        IOptions<JwtOptions> options,
        IMapper _mapper): IServiceManager
    {
        public IProductService ProductService { get; } =new ProductService(_uniteOfWork, _mapper);

        public IBasketService basketService { get; } = new BasketService(basketRepository,_mapper);

        public ICashService cashService { get; } = new CashService(cashRepository);

        public IAuthService authService { get; } = new AuthService(userManager,options);

        public IOrderService OrderService { get; } = new OrderService(_uniteOfWork,_mapper,_basketRepository);
    }
}
