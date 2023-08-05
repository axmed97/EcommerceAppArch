using Core.Utilities.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.ProductDTOs;
using static Entities.DTOs.CategoryDTOs.CategoryDTO;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IResult AddCategory(CategoryAddDTO category);
        IResult DeleteCategory(Category category);
        IResult UpdateCategory(Category category);
        IResultData<List<CategoryHomeListDTO>> GetAllCategories(string langCode);
        List<Category> GetAllNavbarCategories();
        Task<IResultData<List<CategoryAdminListDTO>>> GetAllAdminCategories(string langCode);
        IResultData<List<CategoryFeaturedDTO>> GetAllFeaturedCategory(string langCode);
        IResultData<IEnumerable<CategoryFilterDTO>> GetAllFilterCategories(string langCode);
        IResultData<int> GetAllCategories();
    }
}
