using Store.Domain.Entities.Products;
using Store.Shard.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Specifications
{
    public class ProductCountSpecifications: BaseSpecifications<int, Product>
    {
        public ProductCountSpecifications(ProductQueryParameters parameters): base(
             P =>

            (!parameters.BrandId.HasValue || P.BrandId == parameters.BrandId)
            &&
            (!parameters.TypeId.HasValue || P.TypeId == parameters.TypeId)
            &&
            (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search.ToLower()))

            )
        {
                
        }
    }
}
