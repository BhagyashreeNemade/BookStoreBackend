using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace Common_Layer.Model
{
    public class MSMQModel
    {
        MessageQueue messageQ = new MessageQueue();
        public void sendData2Queue(string token)
        {
            messageQ.Path = @".\private$\Messages";//Setting the QueuPath where we want to store the messages
            if (!MessageQueue.Exists(messageQ.Path))
            {
                MessageQueue.Create(messageQ.Path);
            }
            messageQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQ.ReceiveCompleted += MessageQ_ReceiveCompleted;
            messageQ.Send(token);
            messageQ.BeginReceive();
            messageQ.Close();
        }

        private void MessageQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = messageQ.EndReceive(e.AsyncResult);
            string token = msg.Body.ToString();
            string subject = "Bookstore Reset Link";
            string Body = token;
            var SMTP = new SmtpClient("Smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("bhagunemade2902@gmail.com", "nnuffsdchlxhqwca"),
                EnableSsl = true
            };
            SMTP.Send("bhagunemade2902@gmail.com", "bhagunemade2902@gmail.com", subject, Body);
            messageQ.BeginReceive();
        }
    }
}
