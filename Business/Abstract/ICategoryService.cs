using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        void AddCategory(CategoryAddDTO category);
        void DeleteCategory(Category category);
        void UpdateCategory(Category category);
        List<CategoryHomeListDTO> GetAllCategories(string langCode);
        List<Category> GetAllNavbarCategories();
    }
}
