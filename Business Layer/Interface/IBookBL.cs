using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interface
{
    public interface IBookBL
    {
        public BookModel AddBook(BookModel book);
        public BookModel UpdateBook(BookModel book);
        public bool DeleteBook(int bookId);
        public List<BookModel> GetAllBooks();
        public BookModel GetBookById(int bookId);
    }
}
