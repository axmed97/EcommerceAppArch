using Business.Abstract;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using Core.Utilities.Concrete.ErrorResult;
using Core.Utilities.Concrete.SuccessResult;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDAL _categoryDAL;

        public CategoryManager(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }

        public IResult AddCategory(CategoryAddDTO category)
        {
            try
            {
                _categoryDAL.AddCategoryByLanguages(category);
                return new SuccessResult("Product Added Successfully!");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }

        public IResult DeleteCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public IResultData<List<CategoryHomeListDTO>> GetAllCategories(string langCode)
        {
            var result = _categoryDAL.GetAllCategoriesLanguages(langCode);
            return new SuccessDataResult<List<CategoryHomeListDTO>>(result, "All Categories");
        }

        public List<Category> GetAllNavbarCategories()
        {
            throw new NotImplementedException();
        }

        public IResult UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
