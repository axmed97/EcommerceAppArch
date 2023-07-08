using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        List<Category> GetAllCategory();
    }
}
