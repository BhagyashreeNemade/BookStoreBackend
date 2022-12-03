using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Model
{
    public class ResetPasswordModel
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
