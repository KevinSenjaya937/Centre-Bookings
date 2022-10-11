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

        [HttpGet]
        public IActionResult AdminLogin(string login)
        {
            if (login.Contains("pass"))
            {
                return Ok("Success");
            } 
            else
            {
                return BadRequest("Failed");
            }
        }
    }
}
