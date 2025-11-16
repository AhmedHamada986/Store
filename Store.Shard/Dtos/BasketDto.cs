using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Shard.Dtos
{
    public class BasketDto
    {
        public int  Id { get; set; }
        public IEnumerable<BasketItemDto> MyProperty { get; set; }
    }
}
