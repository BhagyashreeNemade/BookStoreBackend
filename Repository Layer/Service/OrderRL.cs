using Common_Layer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Repository_Layer.Service
{
    public class OrderRL : IOrderRL
    {
        private SqlConnection sqlConnection;
        public IConfiguration Configuration { get; }
        public OrderRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddOrder(OrderModel order)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "sp_AddingOrders";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", order.UserId);
                    sqlCommand.Parameters.AddWithValue("@AddressId", order.AddressId);
                    sqlCommand.Parameters.AddWithValue("@BookId", order.BookId);
                    sqlCommand.Parameters.AddWithValue("@BookQuantity", order.BookQuantity);

                    sqlConnection.Open();
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 2)
                    {
                        return "BookId not exists";
                    }
                    else if (result == 1)
                    {
                        return "UserId not exists";
                    }
                    else
                    {
                        return "Ordered successfully";
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<GetOrderModel> AllOrderDetails(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "sp_GetAllOrders";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    List<GetOrderModel> order = new List<GetOrderModel>();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            GetOrderModel orderModel = new GetOrderModel();
                            BookGetOrderModel getbookModel = new BookGetOrderModel();
                            getbookModel.BookId = Convert.ToInt32(sqlData["BookId"]);
                            getbookModel.BookName = sqlData["BookName"].ToString();
                            getbookModel.Author = sqlData["Author"].ToString();
                            getbookModel.DiscountPrice = Convert.ToInt32(sqlData["DiscountPrice"]);
                            getbookModel.ActualPrice = Convert.ToInt32(sqlData["ActualPrice"]);
                            getbookModel.BookDetail = sqlData["BookDetail"].ToString();
                            getbookModel.BookImage = sqlData["BookImage"].ToString();
                            orderModel.OrderId = Convert.ToInt32(sqlData["OrdersId"]);
                            //orderModel.UserId = Convert.ToInt32(sqlData["UserId"]);
                            //orderModel.AddressId = Convert.ToInt32(sqlData["AddressId"]);
                            //orderModel.BookId = Convert.ToInt32(sqlData["BookId"]);
                            //orderModel.BookQuantity = Convert.ToInt32(sqlData["BookQuantity"]);
                            orderModel.OrderDate = sqlData["OrderDate"].ToString();
                            orderModel.getbookModel = getbookModel;
                            order.Add(orderModel);
                        }
                        return order;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public string DeleteOrder(int OrdersId, int userId)
        {
            this.sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spRemoveFromOrder", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@OrdersId", OrdersId);
                    cmd.Parameters.AddWithValue("@UserId", userId);


                    sqlConnection.Open();
                    var result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();

                    if (result != 0)
                    {
                        return "Order Deleted Successfully";
                    }
                    else
                    {
                        return "Failed to Delete the Order";
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