using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.OrderDTO
{
    public class OrderCreateDTO
    {
        public string UserId { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string Message { get; set; }
    }
}
