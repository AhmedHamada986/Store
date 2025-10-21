using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Contracts
{
    public interface IUniteOfWork
    {
        // Generate Repository 

        IGenericRepository<TKey, TEntity> GetRepository<TKey,TEntity>()where TEntity: BaseEntity<TKey>;

        // save Changes
        Task<int> SaveChangesAsync();
    }
}
