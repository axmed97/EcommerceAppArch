using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.CategoryDTOs
{
    public class CategoryAddDTO
    {
        public string PhotoUrl { get; set; }
        public List<string> CategoryName { get; set; }
        public List<string> LangCode { get; set; }
    }
}
