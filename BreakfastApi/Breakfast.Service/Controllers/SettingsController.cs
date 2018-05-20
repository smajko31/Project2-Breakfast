﻿using Breakfast.Business;
using Breakfast.Business.Weather;
using Breakfast.Business.Weather.Models;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace Breakfast.Service.Controllers
{
    public class SettingsController : ApiController
    {
        [HttpGet]
        [ResponseType(typeof(void))]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("test/testresponse")]
        public IHttpActionResult TestResponse()
        {
            return Ok();
        }

        [HttpGet]
        [ResponseType(typeof(SettingsModel))]
        [Route("api/settings/get/{userId}")]
        public IHttpActionResult GetSettings(string userId)
        {
            SettingsModel settings = Settings.getSettings(userId);
            try { return Ok(settings); }
            catch { return InternalServerError(); }
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        [Route("api/settings/initialize/{userId}")]
        public IHttpActionResult InitializeSettings(string userId)
        {
            try { Settings.initializeSettings(userId); return StatusCode(HttpStatusCode.Created); }
            catch { return InternalServerError(); }
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        [Route("api/settings/weather/{userId}")]
        public IHttpActionResult SaveWeatherSettings(string userId, WeatherSettings ws)
        {
            try { WeatherCrud.SaveSettings(userId, ws); return StatusCode(HttpStatusCode.Accepted); }
            catch { return InternalServerError(); }
        }
    }
}
