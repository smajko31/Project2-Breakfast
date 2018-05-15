﻿using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

namespace Breakfast.Areas.Weather.Models
{
    public class OpenWeatherMap
    {
        public string city { get; set; }
        public string country { get; set; }
        public string description { get; set; }
        public double windSpeed { get; set; }
        public double temperature { get; set; }
        public char temperatureType { get; set; }
        public int humidity { get; set; }
        public int cloudiness { get; set; }
        public string icon { get; set; }

        public void GetResponse(string zipcode)
        {
            string apiKey = "6e0c425a741c67ae35eda9b8812c60b8";
            HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?zip=" + zipcode.ToString() + "&appid=" + apiKey + "&units=imperial") as HttpWebRequest;
           
            string apiResponse = "";
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }

            JsonResponseHelpers.CurrentWeather.ResponseWeather rootObject = JsonConvert.DeserializeObject<JsonResponseHelpers.CurrentWeather.ResponseWeather>(apiResponse);

            city = rootObject.name;
            country = rootObject.sys.country;
            description = rootObject.weather[0].description;
            windSpeed = rootObject.wind.speed;
            temperature = rootObject.main.temp;
            humidity = rootObject.main.humidity;
            cloudiness = rootObject.clouds.all;
        }
    }
}