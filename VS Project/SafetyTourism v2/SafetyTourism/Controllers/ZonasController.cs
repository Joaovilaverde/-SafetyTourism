using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SafetyTourism.Controllers
{
    public class ZonasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
