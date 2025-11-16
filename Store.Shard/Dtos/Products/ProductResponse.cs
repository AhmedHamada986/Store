using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Store.Shard.Dtos.Products
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PicturUrl  { get; set; }

        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }
}
