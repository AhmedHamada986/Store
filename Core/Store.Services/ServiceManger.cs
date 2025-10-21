using AutoMapper;
using Store.Domain.Contracts;
using Store.Services.Abstraction;
using Store.Services.Abstraction.Products;
using Store.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services
{
    public class ServiceManger (IUniteOfWork _uniteOfWork, IMapper _mapper): IServiceManager
    {
        public IProductService ProductService { get; } =new ProductService(_uniteOfWork, _mapper);
    }
}
