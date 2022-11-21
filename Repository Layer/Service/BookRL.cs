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

    public class BookRL : IBookRL
    {
        private readonly IConfiguration configuration;
        SqlConnection con;

        public BookRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public BookModel AddBook(AddBook addBook)
        {

            this.con = new SqlConnection(this.configuration.GetConnectionString("BookStore"));
            using (con)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAddBook", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookName", addBook.BookName);
                    cmd.Parameters.AddWithValue("@Author", addBook.Author);
                    cmd.Parameters.AddWithValue("@BookImage", addBook.BookImage);
                    cmd.Parameters.AddWithValue("@BookDetail", addBook.BookDetail);
                    cmd.Parameters.AddWithValue("@DiscountPrice", addBook.DiscountPrice);
                    cmd.Parameters.AddWithValue("@ActualPrice", addBook.ActualPrice);
                    cmd.Parameters.AddWithValue("@Quantity", addBook.Quantity);
                    cmd.Parameters.AddWithValue("@Rating", addBook.Rating);
                    cmd.Parameters.AddWithValue("@RatingCount", addBook.RatingCount);
                    cmd.Parameters.Add("@BookId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    int bookId = Convert.ToInt32(cmd.Parameters["@BookId"].Value.ToString());
                    con.Close();

                    if (result != 0)
                    {
                        BookModel bookModel = new BookModel
                        {
                            BookId = bookId,
                            BookName = addBook.BookName,
                            Author = addBook.Author,
                            BookImage = addBook.BookImage,
                            BookDetail = addBook.BookDetail,
                            DiscountPrice = addBook.DiscountPrice,
                            ActualPrice = addBook.ActualPrice,
                            Quantity = addBook.Quantity,
                            Rating = addBook.Rating,
                            RatingCount = addBook.RatingCount
                        };
                        return bookModel;
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
    }
}