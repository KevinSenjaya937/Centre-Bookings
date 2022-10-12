using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Website_Frontend.Models;
using Newtonsoft.Json;

namespace Website_Frontend.Controllers
{
    public class SearchCentres : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "SearchCentres";
            return View(new List<Centre>());
        }

        public IActionResult SearchAllCentres(string searchValue)
        {

            RestClient restClient = new RestClient("http://localhost:49987/");
            RestRequest restRequest = new RestRequest("api/CentresDB/", Method.Get);
            RestResponse restResponse = restClient.Execute(restRequest);

            List<Centre> centreList = JsonConvert.DeserializeObject<List<Centre>>(restResponse.Content);

            IList<Centre> matchingList = new List<Centre>();

            foreach (Centre center in centreList)
            {
                if (center.Name.Contains(searchValue))
                {
                    matchingList.Add(center);
                }
            }
            return View(matchingList);
        }
    }
}
