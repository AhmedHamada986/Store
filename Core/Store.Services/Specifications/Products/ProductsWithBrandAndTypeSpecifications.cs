using Store.Domain.Entities.Products;
using Store.Shard.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Specifications.Products
{
    public class ProductsWithBrandAndTypeSpecifications:BaseSpecifications<int , Product>
    {

        public ProductsWithBrandAndTypeSpecifications(int id ):base(P=>P.Id==id)
        {
            //    Include.Add(P => P.Brand);
            //    Include.Add(P => P.Type);
            ApplyIncludes();
        }
        public ProductsWithBrandAndTypeSpecifications(ProductQueryParameters parameters) :base
            (
            
            P=>

            (!parameters.BrandId.HasValue || P.BrandId== parameters.BrandId)
            && 
            (!parameters.TypeId.HasValue || P.TypeId == parameters.TypeId)
            &&
            (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search.ToLower()))
            )
        {
            //    Include.Add(P => P.Brand);
            //    Include.Add(P => P.Type);


            //     PageIndex =3 
            //     PageSize  = 5
            //     skip = 2* 5  (pageIndex - 1 )* page size 
            //     Take  = pagesize 
            ApplyPagination(parameters.PageSize, parameters.PageIndex);
            ApplySorting(parameters.Sort);
            ApplyIncludes();

        }

        private void ApplyIncludes() {
            Include.Add(P => P.Brand);
            Include.Add(P => P.Type);
        }

        private void ApplySorting(string? sort) {

            if (!string.IsNullOrEmpty(sort))
            {
                //check 
                switch (sort.ToLower())
                {

                    case "priceasc":
                        /*OrderBy = P => P.Price;*/
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        //OrderByDescending = P => P.Price;
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        OrderBy = P => P.Name;
                        AddOrderBy(P => P.Name);
                        break;

                }


            }
            else
            {

                //OrderBy = P => P.Name;
                AddOrderBy(P => P.Name);
            }
        }
    }
}
