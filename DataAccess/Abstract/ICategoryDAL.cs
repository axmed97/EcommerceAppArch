using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;

namespace DataAccess.Abstract
{
    // DAL - Data Access Layer
    public interface ICategoryDAL : IRepositoryBase<Category>
    {
        List<CategoryHomeListDTO> GetAllCategoriesLanguages(string langCode);
    }
}
