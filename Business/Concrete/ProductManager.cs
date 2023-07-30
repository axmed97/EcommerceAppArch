using Business.Abstract;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete.ErrorResult;
using Core.Utilities.Concrete.SuccessResult;
using DataAccess.Abstract;
using Entities.DTOs.ProductDTOs;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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
    }
}
