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
        private readonly IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }

        public string PlaceOrder(PlaceOrderModel order, int userId)
        {
            try
            {
                return orderRL.PlaceOrder(order, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<OrderModel> GetAllOrders(int userId)
        {
            try
            {
                return orderRL.GetAllOrders(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveOrder(int orderId)
        {
            try
            {
                return orderRL.RemoveOrder(orderId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}