using Common_Layer.Model;
using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IUserRL
    {
        public UserRegisterModel Register(UserRegisterModel user);
        public LoginResponseModel Login(LoginModel user);
        public string ForgotPassword(string emailId);
        public string ResetPassword(ResetPasswordModel user);
    }
}