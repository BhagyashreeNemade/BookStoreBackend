using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Model
{
    public class BookModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string BookImage { get; set; }
        public string BookDetail { get; set; }
        public double DiscountPrice { get; set; }
        public double ActualPrice { get; set; }
        public int Quantity { get; set; }
        public double Rating { get; set; }
        public int RatingCount { get; set; }
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