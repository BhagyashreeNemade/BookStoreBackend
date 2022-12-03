using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interface
{
    public interface IUserBL
    {
        UserRegisterModel Register(UserRegisterModel user);
        public LoginResponseModel Login(LoginModel user);
        public string ForgotPassword(string emailId);
        public string ResetPassword(ResetPasswordModel user);
    }
}
