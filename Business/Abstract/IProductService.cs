using Core.Utilities.Abstract;
using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IResultData<List<ProductAdminListDTO>> GetDashboardProducts(string userId, string langCode);
    }
}
