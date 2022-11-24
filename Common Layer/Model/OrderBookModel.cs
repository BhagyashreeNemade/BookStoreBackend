using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Model
{
    public class OrderBookModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public int DiscountPrice { get; set; }
        public int ActualPrice { get; set; }
        public string BookImage { get; set; }
    }
}