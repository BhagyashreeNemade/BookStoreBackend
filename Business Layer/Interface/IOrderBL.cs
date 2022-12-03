using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interface
{
    public interface IOrderBL
    {
        public string PlaceOrder(PlaceOrderModel order, int userId);
        public List<OrderModel> GetAllOrders(int userId);
        public bool RemoveOrder(int orderId);

    }
}