using AutoMapper;
using Store.Domain.Contracts;
using Store.Domain.Entities.Products;
using Store.Services.Abstraction.Products;
using Store.Services.Specifications;
using Store.Services.Specifications.Products;
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
        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync(int? brandId, int?typeId, string? sort,string?search )
        {
            //var spec = new BaseSpecifications<int, Product>(null);
            //spec.Include.Add(P=>P.Brand);
            //spec.Include.Add(P=>P.Type);

            var spec = new ProductsWithBrandAndTypeSpecifications(brandId, typeId,sort,search);

            
            var products = await _uniteOfWork.GetRepository<int, Product>().GetAllAsync(spec);
            var result = _mapper.Map<IEnumerable<ProductResponse>>(products); 
            return result;
        }


        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var spec = new ProductsWithBrandAndTypeSpecifications(id);
            var product = await _uniteOfWork.GetRepository<int, Product>().GetAsync(spec);
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
