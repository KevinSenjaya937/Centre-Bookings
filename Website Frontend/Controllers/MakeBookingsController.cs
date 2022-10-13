using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Website_Frontend.Models;
using Newtonsoft.Json;

namespace Website_Frontend.Controllers
{
    public class MakeBookingsController : Controller
    {
        RestClient restClient = new RestClient("http://localhost:49987/");
        public IActionResult Index()
        {
            ViewBag.Title = "MakeBookings";
            
            RestRequest restRequest = new RestRequest("api/CentresDB/", Method.Get);
            RestResponse restResponse = restClient.Execute(restRequest);

            List<Centre> centreList = JsonConvert.DeserializeObject<List<Centre>>(restResponse.Content);

            ViewBag.Centres = centreList;
            return View();
        }

        [HttpPost]
        public IActionResult Insert([FromBody] Booking booking)
        {
            DateTime today = DateTime.Today;
            if (booking.StartDate > booking.EndDate)
            {
                return BadRequest("End Date cannot be before Start Date");
            }
            else if (booking.StartDate < today)
            {
                return BadRequest("No back dates are allowed for Start Date");
            }
            else
            {
                if (SearchDateOverlap(booking))
                {
                    return BadRequest("Requested Booking Overlaps Another Booking!");
                }
                else
                {
                    RestRequest restRequest = new RestRequest("api/Bookings/", Method.Post);
                    restRequest.AddJsonBody(JsonConvert.SerializeObject(booking));
                    RestResponse restResponse = restClient.Execute(restRequest);

                    if (restResponse.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return Ok("Successfully Added");
                    }
                    else if (restResponse.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        return Conflict("Matching Booking ID Found! Please change ID to add booking.");
                    }
                    else
                    {
                        return BadRequest("Failed Adding Booking");
                    }
                }
            }
            
                
        }

        public Boolean SearchDateOverlap(Booking customerBooking)
        {
            DateTime requestedStart = customerBooking.StartDate;
            DateTime requestedEnd = customerBooking.EndDate;
            string centreName = customerBooking.CentreName;
            Boolean overlaps = false;

            RestRequest restRequest = new RestRequest("api/Bookings/", Method.Get);
            RestResponse restResponse = restClient.Execute(restRequest);

            List<Booking> bookingList = JsonConvert.DeserializeObject<List<Booking>>(restResponse.Content);

            List<Booking> matchingBookings = new List<Booking>();

            foreach (Booking booking in bookingList)
            {
                if (booking.CentreName == centreName)
                {
                    matchingBookings.Add(booking);
                }
            }

            foreach (Booking booking in matchingBookings)
            {
                if (requestedStart <= booking.EndDate && requestedEnd >= booking.StartDate)
                {
                    overlaps = true;
                }
            }

            return overlaps;
        }
    }
}
