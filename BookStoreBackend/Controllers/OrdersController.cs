using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using System;
using Business_Layer.Interface;
using Common_Layer.Model;

namespace BookStoreBackend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderBL orderBL;

        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

        [HttpPost]
        [Route("addOrders")]
        public IActionResult AddOrder(OrderModel order)
        {
            try
            {
                string result = this.orderBL.AddOrder(order);
                if (result.Equals("Ordered successfully"))
                {

                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("GetOrders/{UserId}")]
        public IActionResult AlOrderDetails(int UserId)
        {
            try
            {
                var result = this.orderBL.AllOrderDetails(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Shown Below", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "There is no order for the User" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }





        [HttpDelete("Delete")]
        public IActionResult DeleteOrder(int OrderId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = orderBL.DeleteOrder(OrderId, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, data = result });

                }
                else
                {
                    return this.BadRequest();
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
