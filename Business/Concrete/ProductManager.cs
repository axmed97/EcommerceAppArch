using Business.Abstract;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete.ErrorResult;
using Core.Utilities.Concrete.SuccessResult;
using DataAccess.Abstract;
using Entities.DTOs.CartDTOs;
using Entities.DTOs.ProductDTOs;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Net.Http.Headers;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDAL _productDAL;
        public ProductManager(IProductDAL productDAL)
        {
            _productDAL = productDAL;
        }

        public IResult AddProductAdmin(string userId, ProductAddDTO productAdd)
        {
            var result = _productDAL.AddProduct(userId, productAdd);
            if (result.Success)
            {
                return new SuccessResult(result.Message);
            }
            return new ErrorResult(result.Message);
        }

        public IResultData<List<ProductFeaturedDTO>> GetAllFeaturedProducts(string langCode)
        {
            var result = _productDAL.GetFeaturedProducts(langCode);
            return new SuccessDataResult<List<ProductFeaturedDTO>>(result.Data);
        }

        public IResultData<IEnumerable<ProductFilteredDTO>> GetAllFilteredProducts(List<int> categoryIds, string langCode, int minPrice, int maxPrice, int pageNo, int take)
        {
            var result = _productDAL.GetProductFiltered(categoryIds, langCode, minPrice, maxPrice, pageNo, take);
            return new SuccessDataResult<IEnumerable<ProductFilteredDTO>>(result);
        }

        public IResultData<List<ProductRecentDTO>> GetAllRecentProducts(string langCode)
        {
            var result = _productDAL.GetRecentProducts(langCode);

            return new SuccessDataResult<List<ProductRecentDTO>>(result.Data);
        }

        public IResultData<List<ProductAdminListDTO>> GetDashboardProducts(string userId, string langCode)
        {
            var result = _productDAL.GetProductByUser(userId, langCode);
            if (result.Success)
            {
                return new SuccessDataResult<List<ProductAdminListDTO>>(result.Data);
            }
            return new ErrorDataResult<List<ProductAdminListDTO>>(result.Message);
        }

        public IResultData<ProductDetailDTO> GetProductById(int id, string langCode)
        {
            var result = _productDAL.GetProductDetail(id, langCode);

            return new SuccessDataResult<ProductDetailDTO>(result);
        }

        public IResultData<int> GetProductCount(double take, List<int> categoryIds)
        {
            // GETALL dan istifade edin
            double res = _productDAL.GetProductCountByCategory(take, categoryIds) / take;
            int productCountResult = (int)Math.Ceiling((double)res);

            return new SuccessDataResult<int>(productCountResult);
        }

        public IResultData<List<UserCartDTO>> GetProductForCart(List<int> ids, string langCode, List<int> quantity)
        {
            var result = _productDAL.GetUserCart(ids, langCode);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Quantity = quantity[i];
            }

            return new SuccessDataResult<List<UserCartDTO>>(result);
        }

        public IResultData<int> GetProductQuantityById(int productId)
        {
            var productCount = _productDAL.Get(x => x.Id == productId).Quantity;
            return new SuccessDataResult<int>(productCount);
        }
    }
}
