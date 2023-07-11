using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        void DeleteCategory(Category category);
        void UpdateCategory(Category category);
        List<Category> GetAllCategories();
        List<Category> GetAllNavbarCategories();
    }
}
