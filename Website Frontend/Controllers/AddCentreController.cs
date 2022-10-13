using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Website_Frontend.Models;
using Newtonsoft.Json;
using System.Net;

namespace Website_Frontend.Controllers
{
    public class AddCentreController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "AddCentre";
            return View();
        }

        [HttpPost]
        public IActionResult AddCentre([FromBody] Centre centre)
        {
            RestClient restClient = new RestClient("http://localhost:49987/");
            RestRequest restRequest = new RestRequest("api/CentresDB/", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(centre));
            RestResponse restResponse = restClient.Execute(restRequest);

            if (restResponse.StatusCode == HttpStatusCode.Created)
            {
                return Ok("Centre Successfully Added");
            }
            else if (restResponse.StatusCode == HttpStatusCode.Conflict)
            {
                return Conflict("Matching Centre ID Found! Please change to add centre.");
            }
            else
            {
                return BadRequest("Failed to add centre to DB.");
            }
        }
    }
}
