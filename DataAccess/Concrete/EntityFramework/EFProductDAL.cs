using Core.DataAccess.SQLServer.EntityFramework;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete.ErrorResult;
using Core.Utilities.Concrete.SuccessResult;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ProductDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFProductDAL : EFRepositoryBase<Product, AppDbContext>, IProductDAL
    {
        public IResultData<List<ProductAdminListDTO>> GetProductByUser(string userId, string? lanCode = "Az")
        {
			try
			{
                using AppDbContext context = new();

                var products = context.Products
                    .Include(x => x.ProductLanguages)
                    .Include(x => x.Pictures)
                    .Include(x => x.Category)
                    .ThenInclude(x => x.CategoryLanguages)
                    .Select(x => new ProductAdminListDTO
                    {
                        ProductName = x.ProductLanguages.FirstOrDefault(x => x.LangCode == lanCode).ProductName,
                        Id = x.Id,
                        Price = x.Price,
                        Discount = x.Discount,
                        CategoryName = x.Category.CategoryLanguages.FirstOrDefault(x => x.LangCode == lanCode).CategoryName,
                        PhotoUrl = x.Pictures.FirstOrDefault().PhotoUrl
                    }).ToList();

                return new SuccessDataResult<List<ProductAdminListDTO>>(products, "Products were delivered successfully");
            }
			catch (Exception ex)
			{
                return new ErrorDataResult<List<ProductAdminListDTO>>(ex.Message);
            }
        }
    }
}
