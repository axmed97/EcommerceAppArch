using Core.Utilities.Abstract;
using Entities.DTOs.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IResult CreateOrder(List<OrderCreateDTO> orderCreateDTO);
    }
}
