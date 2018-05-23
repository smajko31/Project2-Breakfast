﻿using Breakfast.Areas.Weather.Models;
using Breakfast.Models;
using Breakfast.ViewModels;
using System.Web.Mvc;

namespace Breakfast.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                Data data = new Data()
                {
                    // get settings for current user
                    settings = new SettingsModel().GetSettings(User.Identity.Name),
                    // get weather data for current user
                    weatherData = new OpenWeatherMap("10305")
                    // get traffic data for current user
                    // TODO
                    // get news data for current user
                    // TODO
                };
                return View(data);
            }
            else
                return View(new Data());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}