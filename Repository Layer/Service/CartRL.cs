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
        public List<CartResponse> GetAllCart(int userId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    List<CartResponse> cartResponses = new List<CartResponse>();
                    SqlCommand cmd = new SqlCommand("spGetAllCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            CartResponse cart = new CartResponse();
                            cart.BookId = Convert.ToInt32(rdr["BookId"]);
                            cart.UserId = Convert.ToInt32(rdr["UserId"]);
                            cart.CartId = Convert.ToInt32(rdr["CartId"]);
                            cart.BookName = Convert.ToString(rdr["BookName"]);
                            cart.Author = Convert.ToString(rdr["Author"]);
                            cart.BookImage = Convert.ToString(rdr["BookImage"]);
                            cart.DiscountPrice = Convert.ToDouble(rdr["DiscountPrice"]);
                            cart.ActualPrice = Convert.ToDouble(rdr["ActualPrice"]);
                            cart.CartsQty = Convert.ToInt32(rdr["CartsQty"]);
                            cart.Stock = Convert.ToInt32(rdr["Quantity"]);
                            cartResponses.Add(cart);
                        }

                        con.Close();
                        return cartResponses;
                    }
                    else
                    {
                        con.Close();
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        public string UpdateQtyInCart(int cartId, int cartQty, int userId)
        {
            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    CartResponse cartResponses = new CartResponse();
                    SqlCommand cmd = new SqlCommand("spUpdateQtyInCart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CartId", cartId);
                    cmd.Parameters.AddWithValue("@CartsQty", cartQty);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return "Quantity updated in Cart successfully";
                    }
                    else
                    {
                        return "Failed to Update Quantity in Cart";
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