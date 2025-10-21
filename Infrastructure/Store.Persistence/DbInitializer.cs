using Microsoft.EntityFrameworkCore;
using Store.Domain.Contracts;
using Store.Domain.Entities.Products;
using Store.Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Store.Persistence
{
    public class DbInitializer(StoreDbContext _context) : IDbInitializer
    {
        
        public async Task InitializeAsync()
        {
            // create data base if not Exists 
            if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {

                await _context.Database.MigrateAsync();

            }

            //Data Seeding 

            if (!_context.ProductBrands.Any())
            {
                // Product Brand 

                // Read All Data from jsom file 
                //C:\Users\GEEKS\source\repos\Store\Infrastructure\Store.Persistence\Data\DataSeeding\brands.json
                    var brandData = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Persistence\Data\DataSeeding\brands.json");
                // 2. convert json string to list<product brand >

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                if (brands is not null && brands.Count > 0)
                {
                    await _context.ProductBrands.AddRangeAsync(brands);

                }

            }

            // Product Type

            if (!_context.ProductTypes.Any())
            {

                var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Persistence\Data\DataSeeding\types.json");

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                if (types is not null && types.Count > 0)
                {

                    await _context.ProductTypes.AddRangeAsync(types);
                }

            }
            ;
            // Products


            if (!_context.Products.Any())
            {

                var productData = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Persistence\Data\DataSeeding\products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productData);

                if (products is not null && products.Count > 0)
                {

                    await _context.Products.AddRangeAsync(products);
                }

            }

            await _context.SaveChangesAsync();
        }
    }
}
