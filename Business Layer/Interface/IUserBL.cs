using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interface
{
    public interface IUserBL
    {
        public RegistrationModel AddUser(RegistrationModel usermodel);
        public string UserLogin(LoginModel login);
        string ForgetPassword(string email);
        public bool ResetPassword(string EmailId, string Password);
    }
}
