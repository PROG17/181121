using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _environment;
        private readonly IConfiguration _config;

        public HomeController(IHostingEnvironment environment, IConfiguration config)
        {
            _environment = environment;
            _config = config;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel();
            model.EnvironmentInfo = _environment.EnvironmentName;

            model.MyKey = _config.GetValue<string>("MyKey");
            model.EmailUsername = _config.GetValue<string>("Email:Username"); // Gå in i sektion Email, hämta Username

            if (_environment.IsDevelopment())
            {
                // Gör någon debug i mijön Development grej...
                model.EnvironmentInfo = model.EnvironmentInfo+ " Gör en Debug grej... ";
            }

            if (_environment.IsEnvironment("Integration"))
            {
                // Gör någon debug i miljön Environment grej...
                model.EnvironmentInfo = model.EnvironmentInfo + " Gör en specialare i Integration... ";
            }


            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            var client = new SmtpClient("smtp.mailtrap.io", 2525) // hostname, port
            {
                // OBS! Skapa konto själv på mailtrap.io och hämta ditt eget username och password.
                Credentials = new NetworkCredential("037b0f20d554a8", "1409040b2f4aef"), // username, password
                EnableSsl = true
            };
            // from, to, subject, text
            client.Send("from@example.com", "to@example.com", "Rubriken på mejlet", "Texten i meddelandet");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
