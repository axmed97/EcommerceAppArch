using Core.DataAccess.SQLServer.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFOrderDAL : EFRepositoryBase<Order, AppDbContext>, IOrderDAL
    {
        public void AddRange(List<Order> orders)
        {
            using AppDbContext context = new();
            context.AddRange(orders);
            context.SaveChanges();
        }
    }
}
