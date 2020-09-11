using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GlobalExpectionFilterDemo.Models;

namespace GlobalExpectionFilterDemo.Controllers
{
        public class HomeController : Controller
        {
                private readonly ILogger<HomeController> _logger;

                public HomeController( ILogger<HomeController> logger )
                {
                        var test1 = 0;
                        var test2 = 100 / test1;
                        _logger = logger;
                }

                public IActionResult Index( )
                {
                        return View();
                }

                public IActionResult Privacy( )
                {
                        return View();
                }

                [ResponseCache(Duration = 0 , Location = ResponseCacheLocation.None , NoStore = true)]
                public IActionResult Error( )
                {
                        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }
        }
}
