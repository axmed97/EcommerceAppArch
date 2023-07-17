using Core.Utilities.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IResult AddCategory(CategoryAddDTO category);
        IResult DeleteCategory(Category category);
        IResult UpdateCategory(Category category);
        IResultData<List<CategoryHomeListDTO>> GetAllCategories(string langCode);
        List<Category> GetAllNavbarCategories();
    }
}
