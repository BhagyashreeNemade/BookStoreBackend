using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Model
{
    public class AddFeedbackModel
    {
        public double Rating { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
    }
}