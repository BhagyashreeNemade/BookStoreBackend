using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Model
{
    public class LoginResponseModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public long MobileNumber { get; set; }
        public string token { get; set; }
    }
}
