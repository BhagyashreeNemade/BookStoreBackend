﻿using Common_Layer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Repository_Layer.Service
{
    public class CartRL : ICartRL
    {
        private readonly IConfiguration configuration;
        SqlConnection con;
        public CartRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public AddToCart AddToCart(AddToCart addCart, int userId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAddToCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CartsQty", addCart.CartsQty);
                    cmd.Parameters.AddWithValue("@BookId", addCart.BookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result > 0)
                    {
                        return addCart;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        public string RemoveFromCart(int cartId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spRemoveFromCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CartId", cartId);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return "Item Removed from cart Successfully";
                    }
                    else
                    {
                        return "Failed to Remove item from cart";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}