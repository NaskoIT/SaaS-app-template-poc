using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MultitenancyAppTemplate.Infrastructure;
using MultitenancyAppTemplate.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyAppTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApplicationDbContextBuilder applicationDbContextBuilder;

        public HomeController(
            ILogger<HomeController> logger,
            IApplicationDbContextBuilder applicationDbContextBuilder)
        {
            _logger = logger;
            this.applicationDbContextBuilder = applicationDbContextBuilder;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            // TODO: the tenantId should be set in the headers or in the connection string and here we should be able to easily get it
            string tenantId = "someTenantId"; // get the tenant id from the headers or
            
            using var context = applicationDbContextBuilder.Build(tenantId);
            int usersCount = context.Users.Count();

            return View(new DashboardViewModel
            {
                UsersCount = usersCount
            });
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
