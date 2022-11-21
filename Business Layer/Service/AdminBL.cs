using Business_Layer.Interface;
using Common_Layer.Model;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Service
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public string AdminLogin(LoginModel login)
        {
            try
            {
                return this.adminRL.AdminLogin(login);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}