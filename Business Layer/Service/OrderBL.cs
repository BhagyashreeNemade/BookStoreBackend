using Business_Layer.Interface;
using Common_Layer.Model;
using Repository_Layer.Interface;
using Repository_Layer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Service
{
    public class OrderBL : IOrderBL
    {
        IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }

        public string AddOrder(OrderModel order)
        {
            try
            {
                return this.orderRL.AddOrder(order);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<GetOrderModel> AllOrderDetails(int UserId)
        {
            try
            {
                return this.orderRL.AllOrderDetails(UserId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public string DeleteOrder(int OrderId, int userId)
        {
            return orderRL.DeleteOrder(OrderId, userId);
        }


    }
}