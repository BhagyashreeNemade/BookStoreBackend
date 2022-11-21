using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interface
{
    public interface IBookBL
    {
        public BookModel AddBook(AddBook addBook);
    }
}
