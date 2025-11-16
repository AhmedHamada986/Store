using System.ComponentModel.DataAnnotations;

namespace Store.Shard.Dtos
{
    public class BasketItemDto
    {

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PicturalUrl { get; set; }
        [Range(0,double.MaxValue)]
        public decimal Price  { get; set; }
        [Range(1,99)]
        public int Quantity { get; set; }   
    }
}