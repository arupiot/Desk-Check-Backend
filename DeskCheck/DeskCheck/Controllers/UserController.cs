using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeskCheck.Models;
using System.IO;
using SendGrid;
using SendGrid.Helpers.Mail;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeskCheck.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly DeskCheckContext _context;
        public UserController(DeskCheckContext context)
        {
            _context = context;
        }

        [HttpPost("sendNotif")]
        public void SendNotif()
        {
            string name = Request.Headers["Name"];
            string content = Request.Headers["Content"];
            Execute(name, content).Wait();
        }

        static async Task Execute(string name, string content)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("Desk@check.com", name);
            var subject = "Issue";
            var to = new EmailAddress("jake.adkin@arup.com", "Jake Adkin");
            var plainTextContent = content;
            var htmlContent = content;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}