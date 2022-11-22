﻿using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface ICartRL
    {
        public AddToCart AddToCart(AddToCart addCart, int userId);
        public string RemoveFromCart(int cartId);
        public List<CartResponse> GetAllCart(int userId); 
        public string UpdateQtyInCart(int cartId, int cartQty, int userId);
    }
}
