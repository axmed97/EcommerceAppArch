using Core.DataAccess;
using Core.Utilities.Abstract;
using Entities.Concrete;
using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDAL : IRepositoryBase<Product>
    {
        IResultData<List<ProductAdminListDTO>> GetProductByUser(string userId, string? lanCode = "Az");
    }
}
