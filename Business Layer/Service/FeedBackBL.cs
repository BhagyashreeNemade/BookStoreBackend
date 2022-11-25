using Business_Layer.Interface;
using Common_Layer.Model;
using Repository_Layer.Interface;
using Repository_Layer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Service
{
    public class FeedBackBL : IFeedBackBL
    {
        private readonly IFeedBackRL feedBackRL;
        public FeedBackBL(IFeedBackRL feedBackRL)
        {
            this.feedBackRL = feedBackRL;
        }
        public AddFeedback AddFeedback(AddFeedback addFeedback, int userId)
        {
            return this.feedBackRL.AddFeedback(addFeedback, userId);
        }
        public List<FeedbackResponse> GetAllFeedbacks(int bookId)
        {
            return feedBackRL.GetAllFeedbacks(bookId);
        }

    }
}