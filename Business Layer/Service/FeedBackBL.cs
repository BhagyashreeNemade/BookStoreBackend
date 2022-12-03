using Business_Layer.Interface;
using Common_Layer.Model;
using Repository_Layer.Interface;
using Repository_Layer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Service
{
    public class FeedbackBL : IFeedbackBL
    {
        private readonly IFeedbackRL feedbackRL;
        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }

        public string AddFeedback(AddFeedbackModel feedback, int userId)
        {
            try
            {
                return feedbackRL.AddFeedback(feedback, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<FeedbackModel> GetFeedback(int bookId)
        {
            try
            {
                return feedbackRL.GetFeedback(bookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}