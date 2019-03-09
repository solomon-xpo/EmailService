using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using EmailService.Models;

namespace EmailService.Controllers
{
    public class EmailController : ApiController
    {
        [HttpPost]
        [Route("sendemail")]
        public IHttpActionResult SendEmail([FromBody]EmailModel emailModel)
        {
            string emailSender = ConfigurationManager.AppSettings["username"].ToString();
            string emailSenderPassword = ConfigurationManager.AppSettings["password"].ToString();
            string emailSenderHost = ConfigurationManager.AppSettings["smtp"].ToString();
            int emailSenderPort = Convert.ToInt16(ConfigurationManager.AppSettings["portnumber"]);
            Boolean emailIsSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSL"]);
            string acceptUrl = ConfigurationManager.AppSettings["url"].ToString()+"/"+emailModel.employeeId+"/" + emailModel.aadharNumber+ "/" + "1";
            string rejectUrl = ConfigurationManager.AppSettings["url"].ToString() + "/" + emailModel.employeeId + "/" + emailModel.aadharNumber + "/" + "0";



            //Fetching Email Body Text from EmailTemplate File.  
            string FilePath = "C:\\codebase\\EmailService\\EmailService\\EmailTemplate.html";
            
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();

            
            MailText = MailText.Replace("[accepturl]", acceptUrl);
            MailText = MailText.Replace("[rejecturl]", rejectUrl);



            string subject = "Email Test";

            //Base class for sending email  
            MailMessage _mailmsg = new MailMessage();

            //Make TRUE because our body text is html  
            _mailmsg.IsBodyHtml = true;

            //Set From Email ID  
            _mailmsg.From = new MailAddress(emailSender);

            //Set To Email ID  
            _mailmsg.To.Add(emailModel.emailId);

            //Set Subject  
            _mailmsg.Subject = subject;

            //Set Body Text of Email   
            _mailmsg.Body = MailText;


            //Now set your SMTP   
            SmtpClient _smtp = new SmtpClient();

            //Set HOST server SMTP detail  
            _smtp.Host = emailSenderHost;

            //Set PORT number of SMTP  
            _smtp.Port = emailSenderPort;

            //Set SSL --> True / False  
            _smtp.EnableSsl = emailIsSSL;

            //Set Sender UserEmailID, Password  
            NetworkCredential _network = new NetworkCredential(emailSender, emailSenderPassword);
            _smtp.Credentials = _network;

            //Send Method will send your MailMessage create above.  
            _smtp.Send(_mailmsg);

            return Ok();

        }
    }
}
