using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interface
{
    public interface IFeedbackBL
    {
        public string AddFeedback(AddFeedbackModel feedback, int userId);
        public List<FeedbackModel> GetFeedback(int bookId);


    }
}