using Business_Layer.Interface;
using Common_Layer.Model;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Service
{
    public class AddressBL : IAddressBL
    {
        private readonly IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public string AddAddress(AddressModel address, int userId)
        {
            try
            {
                return addressRL.AddAddress(address, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AddressModel UpdateAddress(AddressModel address, int userId)
        {
            try
            {
                return addressRL.UpdateAddress(address, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<AddressModel> GetAllAddress(int userId)
        {
            try
            {
                return addressRL.GetAllAddress(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}