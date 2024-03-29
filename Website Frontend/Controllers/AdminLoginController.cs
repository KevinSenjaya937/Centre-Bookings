﻿using Microsoft.AspNetCore.Mvc;

namespace Website_Frontend.Controllers
{
    public class AdminLoginController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "AdminLogin";
            return View();
        }

        [HttpGet]
        public IActionResult AdminLogin(string login)
        {
            if (login.Contains("pass"))
            {
                return Ok("Successully Logged In.");
            }
            else
            {
                return BadRequest("Username or Password is incorrect.");
            }
        }
    }
}
