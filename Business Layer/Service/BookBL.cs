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
        public BookModel AddBook(AddBook addBook)
        {
            try
            {
                return this.bookRL.AddBook(addBook);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<BookModel> GetAllBooks()
        {
            return this.bookRL.GetAllBooks();
        }
        public BookModel GetBookById(int bookId)
        {
            return this.bookRL.GetBookById(bookId);
        }
    }
}
