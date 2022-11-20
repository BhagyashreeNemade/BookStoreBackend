using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IUserRL
    {
        public bool Registration(RegisterModel model);
        public string UserLogin(LoginModel loginModel);
        public string ForgetPassword(string Email);
    }
}
