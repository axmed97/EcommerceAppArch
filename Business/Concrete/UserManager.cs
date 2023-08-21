using Business.Abstract;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete.SuccessResult;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDAL _userDAL;

        public UserManager(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }

        public IResultData<User> GetUserById(string userId)
        {
            using AppDbContext context = new();
            
            var result = _userDAL.Get(x => x.Id == userId);
            return new SuccessDataResult<User>(result);
        }
    }
}
