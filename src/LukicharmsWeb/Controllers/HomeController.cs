using Microsoft.AspNetCore.Mvc;
using LukicharmsWeb.Models;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System;
using MailKit.Security;

namespace LukicharmsWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            var model = new ContactModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Contact(ContactModel contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(contact.Name, contact.Email));
                    message.To.Add(new MailboxAddress("Lukicharms", "tynamtd@gmail.com"));
                    message.Subject = contact.Name + " from " + contact.Company + " sent you a message";

                    message.Body = new TextPart("plain")
                    {
                        Text = contact.Message
                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false); //587

                        // Note: since we don't have an OAuth2 token, disable
                        // the XOAUTH2 authentication mechanism.
                        client.AuthenticationMechanisms.Remove("XOAUTH2");

                        // Note: only needed if the SMTP server requires authentication
                        client.Authenticate("username@email.com", "password");

                        client.Send(message);
                        client.Disconnect(true);
                    }

                    ViewBag.Message = "Message Sent";
                    ViewBag.MessageClass = "alert alert-dismissible alert-success";

                    return View(new ContactModel());
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Sorry! Something went wrong and we couldn't send your message. Please try again later. ";
                    ViewBag.MessageClass = "alert alert-dismissible alert-danger";
                    return View(contact);
                }
            }
            else
            {
                ViewBag.Message = "All fields are required";
                ViewBag.MessageClass = "alert alert-dismissible alert-danger";
                return View(contact);
            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
