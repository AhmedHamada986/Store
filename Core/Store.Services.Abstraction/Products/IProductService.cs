using Store.Shard;
using Store.Shard.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Abstraction.Products
{
    public interface IProductService
    {

       Task<PaginationResponse<ProductResponse>>   GetAllProductsAsync(ProductQueryParameters parameters );

        Task<ProductResponse> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync( );
        Task<IEnumerable<BrandTypeResponse>> GetAllTypessAsync( );
    }
}
