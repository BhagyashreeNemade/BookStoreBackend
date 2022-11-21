using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IUserRL
    {
        public RegistrationModel AddUser(RegistrationModel usermodel);
        public string UserLogin(LoginModel login);
        string ForgetPassword(string email);
        public bool ResetPassword(string EmailId, string Password);
    }
}
