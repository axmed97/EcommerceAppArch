using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Business;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete.ErrorResult;
using Core.Utilities.Concrete.SuccessResult;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDAL _orderDAL;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public OrderManager(IOrderDAL orderDAL, IMapper mapper, IProductService productService)
        {
            _orderDAL = orderDAL;
            _mapper = mapper;
            _productService = productService;
        }

        public IResult CreateOrder(List<OrderCreateDTO> orderCreateDTO)
        {
            //if (!CheckProductQuantity(orderCreateDTO).Success)
            //{
            //    if (!CheckProductQuantityLimit(orderCreateDTO).Success)
            //    {
            //        return new ErrorResult();
            //    }
            //}

            //var result = BusinessRules.Check(CheckProductQuantity(orderCreateDTO), CheckProductQuantityLimit(orderCreateDTO));

            //if (!result.Success)
            //{
                var map = _mapper.Map<List<Order>>(orderCreateDTO);
                _orderDAL.AddRange(map);
                return new SuccessResult();
            //}
            return new ErrorResult(Messages.ProductNotFound);
            
        }

        private IResult CheckProductQuantity(List<OrderCreateDTO> orderCreateDTOs)
        {
            foreach (var item in orderCreateDTOs)
            {
                var result = _productService.GetProductQuantityById(item.ProductId);

                if(result.Data == 0)
                {
                    return new ErrorResult();
                }
            }
            return new SuccessResult();
        }

        private IResult CheckProductQuantityLimit(List<OrderCreateDTO> orderCreateDTOs)
        {
            foreach (var item in orderCreateDTOs)
            {
                if (item.Quantity > 10)
                {
                    return new ErrorResult();
                }
            }
            return new SuccessResult();
        }
    }
}
