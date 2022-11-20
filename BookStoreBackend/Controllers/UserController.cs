using Business_Layer.Interface;
using Common_Layer.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iUserBl;

        public UserController(IUserBL iUserBl)
        {
            this.iUserBl = iUserBl;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Registration(RegisterModel userRegistration)
        {
            try
            {
                var result = this.iUserBl.Registration(userRegistration);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration Successfull" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration UnSuceessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult UserLogin(LoginModel loginModel)
        {
            try
            {
                var result = iUserBl.UserLogin(loginModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login Failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ForgetPassword/{EmailId}")]
        public IActionResult ForgetPassword(string EmailId)
        {
            try
            {
                var result = this.iUserBl.ForgetPassword(EmailId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Mail Sent Successful" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Mail UnSuceessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }


    }
}
