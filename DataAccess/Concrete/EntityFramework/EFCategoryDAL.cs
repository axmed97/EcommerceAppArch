using Core.DataAccess.SQLServer.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCategoryDAL : EFRepositoryBase<Category, AppDbContext>, ICategoryDAL
    {
        public List<CategoryHomeListDTO> GetAllCategoriesLanguages(string langCode)
        {
            using var context = new AppDbContext();
            return context.CategoryLanguages
                .Where(x => x.LangCode == langCode)
                .Include(x => x.Category)
                .Select(x => new CategoryHomeListDTO
                {
                    Id = x.Category.Id,
                    CategoryName = x.CategoryName,
                    SeoUrl = x.SeoUrl,
                    PhotoUrl = x.Category.PhotoUrl,
                    ProductCount = 0
                }).ToList();
        }
    }
}
