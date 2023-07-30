using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductDTOs
{
    public class ProductAddDTO
    {
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsFeatured { get; set; }
        public string UserId { get; set; }
        public List<string> ProductName { get; set; }
        public List<string> Description { get; set; }
        public List<string> LangCode { get; set; }
    }
}
