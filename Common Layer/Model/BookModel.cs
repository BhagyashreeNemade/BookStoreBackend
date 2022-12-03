using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Model
{
    public class BookModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public double Rating { get; set; }
        public int ReviewerCount { get; set; }
        public int DiscountPrice { get; set; }
        public int OriginalPrice { get; set; }
        public string BookDetail { get; set; }
        public string BookImage { get; set; }
        public int BookQuantity { get; set; }
    }
    public class BookModelForGetOrder
    {
        // bookName, authorName, DiscountPrice, OriginalPrice, bookImage, bookId
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public int DiscountPrice { get; set; }
        public int ActualPrice { get; set; }
        public string BookImage { get; set; }

    }
}