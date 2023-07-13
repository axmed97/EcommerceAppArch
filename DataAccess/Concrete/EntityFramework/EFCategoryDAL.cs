using Core.DataAccess.SQLServer.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCategoryDAL : EFRepositoryBase<Category, AppDbContext>, ICategoryDAL
    {
        // Error and Exception
        public async Task<bool> AddCategoryByLanguages(CategoryAddDTO categoryAddDTO)
        {
            try
            {
                using var context = new AppDbContext();
                Category category = new()
                {
                    PhotoUrl = "/",
                    IsFeatured = false,
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                for (int i = 0; i < categoryAddDTO.CategoryName.Count; i++)
                {
                    CategoryLanguage categoryLanguage = new()
                    {
                        CategoryName = categoryAddDTO.CategoryName[i],
                        CategoryId = category.Id,
                        LangCode = categoryAddDTO.LangCode[i],
                        SeoUrl = "/"
                    };
                    await context.CategoryLanguages.AddAsync(categoryLanguage);
                }
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

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
