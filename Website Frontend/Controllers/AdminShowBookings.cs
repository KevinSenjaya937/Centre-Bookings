using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Website_Frontend.Models;
using Newtonsoft.Json;

namespace Website_Frontend.Controllers
{
    public class AdminShowBookings : Controller
    {
        RestClient restClient = new RestClient("http://localhost:49987/");
        public IActionResult Index()
        {

            ViewBag.Title = "AdminShowBookings";
            
            RestRequest restRequest = new RestRequest("api/CentresDB/", Method.Get);
            RestResponse restResponse = restClient.Execute(restRequest);

            List<Centre> centreList = JsonConvert.DeserializeObject<List<Centre>>(restResponse.Content);

            ViewBag.Centres = centreList; 

            return View();
        }

        [HttpGet]
        public IActionResult GetBookings(string centreName)
        {
            RestRequest restRequest = new RestRequest("api/Bookings/", Method.Get);
            RestResponse restResponse = restClient.Execute(restRequest);

            List<Booking> bookingList = JsonConvert.DeserializeObject<List<Booking>>(restResponse.Content);

            List<Booking> matchingBooking = new List<Booking>();

            foreach (Booking booking in bookingList)
            {
                if (booking.CentreName == centreName)
                {
                    matchingBooking.Add(booking);
                }
            }

            if (matchingBooking.Count > 0)
            {
                return Ok(JsonConvert.SerializeObject(matchingBooking));
            }
            else
            {
                return BadRequest("This centre has no bookings.");
            }
        }
    }
}
