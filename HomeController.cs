using log4net;
using PersonalV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace PersonalV2.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel contact)
        {
            string body = string.Format($"Name: {contact.Name}<br>Email: {contact.Email}<br>" +
                $"Subject: {contact.Subject}<br>Message: {contact.Message}");

            MailMessage msg = new MailMessage(
                "no-reply@jackie13.com",
                "jnmccla@outlook.com",
                contact.Subject + " - " + DateTime.Now,
                body);

            msg.Priority = MailPriority.High;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("mail.jackie13.com");
            client.Credentials = new NetworkCredential("no-reply@jackie13.com", "Th1nK313");
            client.Port = 8889;

            using (client)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        client.Send(msg);
                    }
                    else
                    {
                        return View();
                    }

                }
                catch(Exception ex)
                {
                    string error = "----------------------- Stack Trace -------------------------" + ex.StackTrace + " ------------------------------ Message ------------------------------" + ex.Message + 
                        " --------------------------------- Help Link -------------------------" + ex.HelpLink + " ------------------------------- Target Site -------------------------" +
                        ex.TargetSite + "------------------------------ Inner -----------------------" + ex.InnerException;
                    
                    ViewBag.ErrorMessage = error;
                    return View();
                }
            }
            return View("ContactConfirmation", contact);
        }


        public ActionResult Portfolio()
        {
            return View();
        }

        public ActionResult Resume()
        {
            return View();
        }

        public ActionResult Links()
        {
            return View();
        }

        public ActionResult TestScript()
        {
            return View();
        }



    }//class
}//namespace