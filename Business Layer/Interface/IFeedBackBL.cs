using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interface
{
    public interface IFeedBackBL
    {
        public AddFeedback AddFeedback(AddFeedback addFeedback, int userId);
        public List<FeedbackResponse> GetAllFeedbacks(int bookId);


    }
}