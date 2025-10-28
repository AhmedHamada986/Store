using Store.Services.Abstraction.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Abstraction
{
    public interface IServiceManager
    {
         IProductService ProductService { get; }
         IBasketService basketService { get;  }
         ICashService cashService { get; }

    }
}
