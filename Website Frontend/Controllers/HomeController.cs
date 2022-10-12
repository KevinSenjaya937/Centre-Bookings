using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Website_Frontend.Models;
using Newtonsoft.Json;

namespace Website_Frontend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            
            return View();
        }
    }
}
