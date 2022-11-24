using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interface
{
    public interface IOrderBL
    {
        public string AddOrder(OrderModel order);
        public List<GetOrderModel> AllOrderDetails(int UserId);
        public string DeleteOrder(int OrderId, int userId);


    }
}