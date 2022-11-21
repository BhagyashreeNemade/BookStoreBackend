using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IBookRL
    {
        public BookModel AddBook(AddBook addBook);
        public List<BookModel> GetAllBooks();
        public BookModel GetBookById(int bookId);
    }
}
