using RealmDigitalAssessment.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RealmDigitalAssessment.BusinessLayer.Helpers
{
    public class SendEmail
    {
        private string server { get; set; }
        private string fromEmailAddress { get; set; }
        private string toEmailAddress { get; set; }
        private string emailBody { get; set; }
        private string emailPassword { get; set; }
        private string emailUsername { get; set; }

        /// <summary>
        /// Send Email Constructor 
        /// </summary>
        public SendEmail() {
            server = ConfigurationManager.AppSettings["EmailServer"];
            fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"];
            toEmailAddress = ConfigurationManager.AppSettings["ToEmailAddress"];
            emailBody = ConfigurationManager.AppSettings["EmailBody"];
            emailPassword = ConfigurationManager.AppSettings["EmailPassword"];
            emailUsername = ConfigurationManager.AppSettings["EmailUsername"];
        }

        /// <summary>
        /// Send birthday email
        /// </summary>
        /// <param name="employee"></param>
        public void SendBirthdayEmail(Employee employee) {           
            
            string to = toEmailAddress;
            string from = fromEmailAddress;
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Happy Birthday";
            message.Body = string.Format(emailBody, employee.Name, employee.Lastname);
            NetworkCredential netCred = new NetworkCredential(emailUsername, emailPassword);
            SmtpClient client = new SmtpClient(server);
            client.UseDefaultCredentials = true;
            client.EnableSsl = true;
            client.Credentials = netCred;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                //Log error to db or file
                throw ex;
            }
        }
    }
}
