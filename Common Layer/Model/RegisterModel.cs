using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Model
{
    public class RegisterModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
    }
}
