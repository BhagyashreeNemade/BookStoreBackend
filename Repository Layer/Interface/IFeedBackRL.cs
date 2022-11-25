using Common_Layer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IFeedBackRL
    {
        public AddFeedback AddFeedback(AddFeedback addFeedback, int userId);
        public List<FeedbackResponse> GetAllFeedbacks(int bookId);


    }
}