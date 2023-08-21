using Core.DataAccess.SQLServer.EntityFramework;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete.ErrorResult;
using Core.Utilities.Concrete.SuccessResult;
using Core.Utilities.SeoUrlHelper;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CartDTOs;
using Entities.DTOs.ProductDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DTOs.ProductDTOs.ProductDTO;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFProductDAL : EFRepositoryBase<Product, AppDbContext>, IProductDAL
    {
        public IResult AddProduct(string userId, ProductAddDTO productAdd)
        {
            try
            {
                using AppDbContext context = new();

                List<Picture> pictureList = new();

                for (int i = 0; i < productAdd.PhotoUrls.Count; i++)
                {
                    pictureList.Add(new Picture { PhotoUrl = productAdd.PhotoUrls[i] });
                }
                

                Product product = new()
                {
                    CategoryId = productAdd.CategoryId,
                    Price = productAdd.Price,
                    Quantity = productAdd.Quantity,
                    Discount = productAdd.Discount,
                    IsFeatured = productAdd.IsFeatured,
                    Pictures = pictureList
                };

                context.Products.Add(product);
                context.SaveChanges();

                for (int i = 0; i < productAdd.ProductName.Count; i++)
                {
                    ProductLanguage productLanguage = new()
                    {
                        ProductId = product.Id,
                        ProductName = productAdd.ProductName[i],
                        Description = productAdd.Description[i],
                        SeoUrl = productAdd.ProductName[i].ReplaceInvalidChars(),
                        LangCode = i == 0 ? "az-Az" : i == 1 ? "ru-Ru" : "en-En"
                    };
                    context.ProductLanguages.Add(productLanguage);
                }
                context.SaveChanges();
                return new SuccessResult("Product added successfully!");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }

            
        }

        public IResultData<List<ProductFeaturedDTO>> GetFeaturedProducts(string langCode)
        {
            using AppDbContext context = new();

            var result = context.Products
                .Include(x => x.ProductLanguages)
                .Include(x => x.Pictures)
                .Where(x => x.IsFeatured == true)
                .Select(x => new ProductFeaturedDTO(
                x.Id,
                x.ProductLanguages.FirstOrDefault(x => x.LangCode == langCode).SeoUrl,  
                x.ProductLanguages.FirstOrDefault(x => x.LangCode == langCode).ProductName,
                x.Price,
                x.Discount,
                x.Pictures.FirstOrDefault().PhotoUrl)).ToList();

            return new SuccessDataResult<List<ProductFeaturedDTO>>(result);


        }

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
                        CategoryName = x.Category.CategoryLanguages.FirstOrDefault(x => x.LangCode == "Az").CategoryName,
                        PhotoUrl = x.Pictures.FirstOrDefault().PhotoUrl
                    }).ToList();

                return new SuccessDataResult<List<ProductAdminListDTO>>(products, "Products were delivered successfully");
            }
			catch (Exception ex)
			{
                return new ErrorDataResult<List<ProductAdminListDTO>>(ex.Message);
            }
        }

        public int GetProductCountByCategory(double take, List<int> categoryIds)
        {

            using var context = new AppDbContext();
            var result = context.Products
                .Where(x => categoryIds.Any() == false ? null == null : categoryIds.Contains(x.CategoryId)).Count();
            return result;
        }

        public ProductDetailDTO GetProductDetail(int id, string langCode)
        {
            using AppDbContext context = new();

            var result = context.Products
                .Select(x => new ProductDetailDTO
                {
                    Id = x.Id,
                    ProductName = x.ProductLanguages.FirstOrDefault(x => x.LangCode == langCode).ProductName,
                    Description = x.ProductLanguages.FirstOrDefault(x => x.LangCode == langCode).Description,
                    Price = x.Price,
                    Discount = x.Discount,
                    Quantity = x.Quantity,
                    PhotoUrls = x.Pictures.Where(x => x.ProductId == id).Select(x => x.PhotoUrl).ToList(),
                }).FirstOrDefault(x => x.Id == id);

            return result;
        }

        public IEnumerable<ProductFilteredDTO> GetProductFiltered(List<int> categoryIds, string langCode, int minPrice, int maxPrice, int pageNo, int take)
        {
            using AppDbContext context = new();

            int next = (pageNo - 1) * take;

            var result = context.Products
                .Include(x => x.ProductLanguages)
                .Include(x => x.Pictures)
                .Where(x => x.Price >= minPrice && x.Price <= maxPrice && (categoryIds.Any()? categoryIds.Contains(x.CategoryId) : null == null))
                .Select(x => new ProductFilteredDTO
                {
                    Id = x.Id,
                    Name = x.ProductLanguages.FirstOrDefault(x => x.LangCode == langCode).ProductName,
                    Price = x.Price,
                    Discount = x.Discount,
                    PhotoUrl = x.Pictures.FirstOrDefault().PhotoUrl
                }).Skip(next).Take(take).ToList();
            
            return result;
        }

        public IResultData<List<ProductRecentDTO>> GetRecentProducts(string langCode)
        {
            using AppDbContext context = new();
            var result = context.Set<ProductRecentDTO>().FromSqlInterpolated($"EXEC GetRecentProducts @LangCode = {langCode}").ToList();
            return new SuccessDataResult<List<ProductRecentDTO>>(result);
        }

        public List<UserCartDTO> GetUserCart(List<int> ids, string langCode)
        {
            using AppDbContext context = new();

            var result = context.Products
                .Where(x => ids.Contains(x.Id))
                .Include(x => x.ProductLanguages)
                .Include(x => x.Pictures)
                .Select(x => new UserCartDTO
                {
                    Id = x.Id,
                    ProductName = x.ProductLanguages.FirstOrDefault(x => x.LangCode == langCode).ProductName,
                    PhotoUrl = x.Pictures.FirstOrDefault().PhotoUrl,
                    Price = x.Price
                }).ToList();
                
            return result;
        }
    }
}
