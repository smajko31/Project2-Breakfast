﻿using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace Breakfast.Areas.Weather.Models
{
    public class OpenWeatherMap
    {
        public WeatherSettings weatherSettings { get; set; }
        public ForecastDay[] forecastDays { get; set; }

        public OpenWeatherMap()
        {
            forecastDays = new ForecastDay[5];
            weatherSettings = new WeatherSettings();
        }

        public OpenWeatherMap(string zipcode)
        {
            forecastDays = new ForecastDay[5];
            weatherSettings = new WeatherSettings();
            GetResponse(zipcode);
        }

        public void GetResponse(string zipcode)
        {
            HttpWebRequest apiRequest = WebRequest.Create("http://ec2-18-188-45-20.us-east-2.compute.amazonaws.com/Breakfast.Service_deploy/" + "api/weather/get/" + zipcode) as HttpWebRequest;

            string apiResponse = "";
            try
            {
                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();
                }
            }
            catch
            {
                weatherSettings = null;
                forecastDays = null;
                return;
            }

            JsonResponseHelpers.WeatherData.RootObject rootObject = JsonConvert.DeserializeObject<JsonResponseHelpers.WeatherData.RootObject>(apiResponse);

            weatherSettings.city = rootObject.city;
            weatherSettings.country = rootObject.country;
            weatherSettings.description = rootObject.description;
            weatherSettings.windSpeed = rootObject.windSpeed;
            weatherSettings.temperature = rootObject.temperature;
            weatherSettings.humidity = rootObject.humidity;
            weatherSettings.cloudiness = rootObject.cloudiness;
            weatherSettings.condition = rootObject.condition;

            int curr = 0;
            foreach (var item in rootObject.forecastDays)
            {
                if (curr < 5)
                    forecastDays[curr] = new ForecastDay
                    {
                        condition = item.condition,
                        day = item.day,
                        tempMax = item.tempMax,
                        tempMin = item.tempMin
                    };
                curr++;
            }
        }

        public void ToCelcius()
        {
            weatherSettings.temperature = FtoC(weatherSettings.temperature);
            foreach (var item in forecastDays)
            {
                item.tempMax = FtoC(item.tempMax);
                item.tempMin = FtoC(item.tempMin);
            }
        }

        private int FtoC(int temp)
        {
            temp -= 32;
            temp *= 5;
            temp /= 9;
            return temp;
        }
    }
}