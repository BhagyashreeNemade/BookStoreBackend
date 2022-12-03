using Business_Layer.Interface;
using Common_Layer.Model;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Service
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        public BookModel AddBook(BookModel book)
        {
            try
            {
                return bookRL.AddBook(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BookModel UpdateBook(BookModel book)
        {
            try
            {
                return bookRL.UpdateBook(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteBook(int bookId)
        {
            try
            {
                return bookRL.DeleteBook(bookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<BookModel> GetAllBooks()
        {
            try
            {
                return bookRL.GetAllBooks();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public BookModel GetBookById(int bookId)
        {
            try
            {
                return bookRL.GetBookById(bookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
