using Core.DataAccess;
using Core.Utilities.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;

namespace DataAccess.Abstract
{
    // DAL - Data Access Layer
    public interface ICategoryDAL : IRepositoryBase<Category>
    {
        List<CategoryHomeListDTO> GetAllCategoriesLanguages(string langCode);
        Task<bool> AddCategoryByLanguages(CategoryAddDTO categoyAddDTO);
        Task<IResultData<List<CategoryAdminListDTO>>> GetAdminAllCategoriesLanguages(string langCode);
        IResultData<List<CategoryFeaturedDTO>> GetFeaturedCategory(string langCode);
    }
}
