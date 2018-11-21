using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
