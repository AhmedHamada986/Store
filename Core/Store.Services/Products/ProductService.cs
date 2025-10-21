using AutoMapper;
using Store.Domain.Contracts;
using Store.Domain.Entities.Products;
using Store.Services.Abstraction.Products;
using Store.Shard.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Products
{
    public class ProductService(IUniteOfWork _uniteOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
        {
            var products = await _uniteOfWork.GetRepository<int, Product>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<ProductResponse>>(products);
            return result;
        }


        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var product = await _uniteOfWork.GetRepository<int, Product>().GetAsync(id);
            var result = _mapper.Map<ProductResponse>(product);
            return result;
        }


        public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync()
        {
            var brands = await _uniteOfWork.GetRepository<int, ProductBrand>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(brands);
            return result;
        }



        public async Task<IEnumerable<BrandTypeResponse>> GetAllTypessAsync()
        {
            var Types = await _uniteOfWork.GetRepository<int, ProductType>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(Types);
            return result;


        }
    }
}
