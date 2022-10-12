using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Website_Frontend.Models;
using Newtonsoft.Json;

namespace Website_Frontend.Controllers
{
    public class AdminCentreListController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "AdminCenterList";

            RestClient restClient = new RestClient("http://localhost:49987/");
            RestRequest restRequest = new RestRequest("api/CentresDB/", Method.Get);
            RestResponse restResponse = restClient.Execute(restRequest);

            List<Centre> centreList = JsonConvert.DeserializeObject<List<Centre>>(restResponse.Content);

            return View(centreList);
            
        }
    }
}
