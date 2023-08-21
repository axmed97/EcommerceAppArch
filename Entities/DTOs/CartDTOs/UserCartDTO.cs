using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.CartDTOs
{
    public class UserCartDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
