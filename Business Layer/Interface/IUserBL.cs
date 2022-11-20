using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interface
{
    public interface IUserBL
    {
        public bool Registration(RegisterModel userRegistrationModel);
        public string UserLogin(LoginModel loginModel);
        public string ForgetPassword(string Email);
    }
}
