using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common_Layer.Model
{
    public class GetOrderModel
    {
        public int OrderId { get; set; }
       
        public string OrderDate { get; set; }
        public int BookQuantity { get; set; }
        public BookGetOrderModel getbookModel { get; set; }
        
    }
}