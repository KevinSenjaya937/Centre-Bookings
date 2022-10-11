using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Website_Frontend.Models;
using Newtonsoft.Json;

namespace Website_Frontend.Controllers
{
    public class CentresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
