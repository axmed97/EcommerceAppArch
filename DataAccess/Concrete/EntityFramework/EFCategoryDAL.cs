using Core.DataAccess.SQLServer.EntityFramework;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete.ErrorResult;
using Core.Utilities.Concrete.SuccessResult;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Microsoft.EntityFrameworkCore;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;

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

        public async Task<IResultData<List<CategoryAdminListDTO>>> GetAdminAllCategoriesLanguages(string langCode)
        {
            using AppDbContext context = new();
            try
            {
                var result = await context.Categories
                    .Select(x => new CategoryAdminListDTO
                    {
                        CategoryName = x.CategoryLanguages.FirstOrDefault(x => x.LangCode == langCode).CategoryName,
                        PhotoUrl = "/",
                        IsFeatured = x.IsFeatured,
                        Id = x.Id,
                        ProductCount = 0
                    }).ToListAsync();

                return new SuccessDataResult<List<CategoryAdminListDTO>>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CategoryAdminListDTO>>(ex.Message);
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

        public IResultData<List<CategoryFeaturedDTO>> GetFeaturedCategory(string langCode)
        {
            using AppDbContext context = new();

            var result = context.Categories
                .Include(x => x.CategoryLanguages)
                .Where(x => x.IsFeatured == true)
                .Select(x => new CategoryFeaturedDTO(x.Id, 
                x.CategoryLanguages.FirstOrDefault(x => x.LangCode == langCode).CategoryName, 
                x.PhotoUrl, 
                x.CategoryLanguages.FirstOrDefault(x => x.LangCode == langCode).SeoUrl,
                0)).ToList();

            return new SuccessDataResult<List<CategoryFeaturedDTO>>(result);
        }
    }
}
