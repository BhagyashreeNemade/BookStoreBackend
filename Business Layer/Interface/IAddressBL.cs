using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interface
{
    public interface IAddressBL
    {
        public string AddAddress(AddressModel address, int userId);
        public AddressModel UpdateAddress(AddressModel address, int userId);
        public List<AddressModel> GetAllAddress(int userId);
    }
}
