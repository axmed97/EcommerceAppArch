using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
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

        public void AddCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAllNavbarCategories()
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
