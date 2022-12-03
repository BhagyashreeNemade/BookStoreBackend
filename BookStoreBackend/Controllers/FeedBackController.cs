using Business_Layer.Interface;
using Common_Layer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using System;

namespace BookStoreBackend.Controllers
{
    [Route("")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL feedbackBL;
        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("Addfeedback")]
        public IActionResult AddFeedback(AddFeedbackModel feedback)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = feedbackBL.AddFeedback(feedback, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to add" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("Getfeedback")]
        public IActionResult GetFeedback(int bookId)
        {
            try
            {
                var result = feedbackBL.GetFeedback(bookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed to get" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}