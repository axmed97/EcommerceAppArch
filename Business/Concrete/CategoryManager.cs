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
using static Entities.DTOs.CategoryDTOs.CategoryDTO;

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

        public async Task<IResultData<List<CategoryAdminListDTO>>> GetAllAdminCategories(string langCode)
        {
            var result = await _categoryDAL.GetAdminAllCategoriesLanguages(langCode);
            if (result.Success)
            {
                return new SuccessDataResult<List<CategoryAdminListDTO>>(result.Data);
            }
            return new ErrorDataResult<List<CategoryAdminListDTO>>(result.Message);
        }

        public IResultData<List<CategoryHomeListDTO>> GetAllCategories(string langCode)
        {
            var result = _categoryDAL.GetAllCategoriesLanguages(langCode);
            return new SuccessDataResult<List<CategoryHomeListDTO>>(result, "All Categories");
        }

        public IResultData<List<CategoryFeaturedDTO>> GetAllFeaturedCategory(string langCode)
        {
            var result = _categoryDAL.GetFeaturedCategory(langCode);
            return new SuccessDataResult<List<CategoryFeaturedDTO>>(result.Data);
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
