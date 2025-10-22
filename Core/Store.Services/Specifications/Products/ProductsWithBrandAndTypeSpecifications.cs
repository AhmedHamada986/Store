using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public ProductsWithBrandAndTypeSpecifications(int? brandId , int? typeId, string? sort, string? search):base
            (
            
            P=>

            (!brandId.HasValue || P.BrandId== brandId)
            && 
            (!typeId.HasValue || P.TypeId == typeId)
            &&
            (string.IsNullOrEmpty(search)|| P.Name.ToLower().Contains(search.ToLower()))
            )
        {
            //    Include.Add(P => P.Brand);
            //    Include.Add(P => P.Type);

            ApplySorting(sort);
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
