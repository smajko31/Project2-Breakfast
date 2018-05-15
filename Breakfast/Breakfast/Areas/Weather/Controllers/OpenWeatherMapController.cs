﻿using Breakfast.Areas.Weather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Breakfast.Areas.Weather.Controllers
{
    public class OpenWeatherMapController : Controller
    {
        // GET: Weather/OpenWeatherMap
        public ActionResult Index()
        {
            return View(new OpenWeatherMap());
        }

        [HttpPost]
        public ActionResult Index(string zipcode)
        {
            OpenWeatherMap owm = new OpenWeatherMap();
            owm.GetResponse(zipcode);
            return View(owm);
        }

        public ActionResult Settings()
        {
            return View();
        }
    }
}