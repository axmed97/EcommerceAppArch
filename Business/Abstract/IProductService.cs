using Core.Utilities.Abstract;
using Entities.DTOs.CartDTOs;
using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace Business.Abstract
{
    public interface IProductService
    {
        IResultData<List<ProductAdminListDTO>> GetDashboardProducts(string userId, string langCode);
        IResult AddProductAdmin(string userId, ProductAddDTO productAdd);
        IResultData<List<ProductFeaturedDTO>> GetAllFeaturedProducts(string langCode);
        IResultData<List<ProductRecentDTO>> GetAllRecentProducts(string langCode);
        IResultData<ProductDetailDTO> GetProductById(int id, string langCode);
        IResultData<IEnumerable<ProductFilteredDTO>> GetAllFilteredProducts(List<int> categoryIds, string langCode, int minPrice, int maxPrice, int pageNo, int take);
        IResultData<int> GetProductCount(double take, List<int> categoryIds);
        IResultData<int> GetProductQuantityById(int productId);
        IResultData<List<UserCartDTO>> GetProductForCart(List<int> ids, string langCode, List<int> quantity);
    }
}
