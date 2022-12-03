using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IFeedbackRL
    {
        public string AddFeedback(AddFeedbackModel feedback, int userId);
        public List<FeedbackModel> GetFeedback(int bookId);


    }
}