using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LukicharmsWeb.Models;

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
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactModel contact)
        {
            var contactInfo = new ContactModel
            {
                Name = contact.Name,
                Email = contact.Email,
                Company = contact.Company,
                Message = contact.Message
            };

            ViewBag.MailSent = true;
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
