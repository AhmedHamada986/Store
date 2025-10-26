using AutoMapper;
using Store.Domain.Contracts;
using Store.Domain.Entities.Products;
using Store.Domain.Exceptions;
using Store.Services.Abstraction.Products;
using Store.Services.Specifications;
using Store.Services.Specifications.Products;
using Store.Shard;
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
        public async Task<PaginationResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParameters parameters)
        {
            //var spec = new BaseSpecifications<int, Product>(null);
            //spec.Include.Add(P=>P.Brand);
            //spec.Include.Add(P=>P.Type);

            var spec = new ProductsWithBrandAndTypeSpecifications( parameters);  
           
            
            var products = await _uniteOfWork.GetRepository<int, Product>().GetAllAsync(spec);
            var result = _mapper.Map<IEnumerable<ProductResponse>>(products);

            var specCount = new ProductCountSpecifications(parameters);

            var count =await _uniteOfWork.GetRepository<int, Product>().CountAsync(specCount);
                
            return new PaginationResponse<ProductResponse>(parameters.PageIndex,parameters.PageSize, count, result);
        }


        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var spec = new ProductsWithBrandAndTypeSpecifications(id);
            var product = await _uniteOfWork.GetRepository<int, Product>().GetAsync(spec);
            if (product is null) throw new ProductNotFoundExceptions(id);
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
