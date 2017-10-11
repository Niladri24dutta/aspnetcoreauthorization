using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AuthorizationLab.Controllers
{
    [Authorize(Policy = "AdministratorOnly")]
    [Authorize(Policy = "EmployeeId")]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
