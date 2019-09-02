using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Website.Model;
using Website.ViewModel;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        private MyInfo _info = new MyInfo();


        //routes 
        [Route("")]
        [Route("[controller]")]
        [Route("[controller]/[action]")]
        public ViewResult Index()
        {


            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel()
            {
                pagetitle = "Hey, it's Andy!",
                name = _info.getName(),
                email = _info.getEmail()
            };
            return View(homeIndexViewModel);
        }
        [HttpPost]
        [Route("[controller]/[action]")]
        public IActionResult Mail(string inputName, string inputEmail, string inputMessage)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(inputEmail));
                message.To.Add(new MailboxAddress("skinybogies76@gmail.com"));
                message.Subject = "Message from: " + inputName;
                message.Body = new TextPart("plain")
                {
                    Text = inputMessage
                };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, true);
                    client.Authenticate("skinybogies76@gmail.com", "Tiger0210606");
                    client.Send(message);
                    client.Disconnect(true);
                };
                return RedirectToAction("Index");
            }
            catch (Exception exp)
            {
                ModelState.Clear();
                ViewBag.Message = $" We have a problem here {exp.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
