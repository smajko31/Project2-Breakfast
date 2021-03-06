﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Breakfast.ViewModels;
using Newtonsoft.Json;

namespace Breakfast.Areas.Traffic.Models
{

    public  class TrafficSettingsViewModel
    {
        const string path = @"http://ec2-18-188-45-20.us-east-2.compute.amazonaws.com/Breakfast.Service_deploy/api/traffic/get/";
        

        public int Id { get; set; }
        public bool Enabled { get; set; } = true;
        public string Address { get; set; } = null;
        public string WorkAddress { get; set; } = null;  
        public bool Driving { get; set; } = true;    
        public string AddressPlaceId { get; set; } = null; 
        public string WorkAddressPlaceId { get; set; } = null;
        public double[] LatLng { get; set; }
        public string UserId { get; set; }
        public string TimeToWork { get; set; }
        

        public TrafficSettingsViewModel()
        {

        }
        public TrafficSettingsViewModel(string userId)
        {

        }
        public static async Task<string> SetTimeToWork(TrafficSettingsViewModel tsvm)
        {
            HttpClient client = new HttpClient();
            string travelMode = (tsvm.Driving) ? "DRIVING" : "WALKING";
            string url = "";
            if (tsvm.AddressPlaceId != null && tsvm.AddressPlaceId.Length > 2 && tsvm.WorkAddressPlaceId != null &&
                tsvm.WorkAddressPlaceId.Length > 2)
            {
                url = path + tsvm.AddressPlaceId + "/" + tsvm.WorkAddressPlaceId + "/" + travelMode + "/" + "1";
            }
            else if (tsvm.Address != null && tsvm.WorkAddress != null)
            {
                string home = ParseAutocomplete(tsvm.Address);
                string work = ParseAutocomplete(tsvm.WorkAddress);
                url = path + home + "/" + work + "/" + travelMode + "/" + "2";
            }
            else
            {
                return "Can't get a time to work without a valid home and work address";
            }

            var response = await client.GetAsync(url);
            string result = null;
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            TimeJsonResponse timeJson = new TimeJsonResponse();
            if (result != null)
            {
                timeJson = JsonConvert.DeserializeObject<TimeJsonResponse>(result);
            }
            else
            {
                return "Could not get time to work, is your address formatted properly?";
            }

            return timeJson.Time;



        }
        private static string ParseAutocomplete(string address)
        {
            string result1, result2;
            result1 = address.Replace(" ", "");
            result2 = result1.Replace(", ", "");
            return result2;
        }
        public string getApiKey()
        {
            return "AIzaSyD-Dndf6f1-0LZCgSB4zaBCqtApaSTIquo";

//            try
//            {
//                string apiKey = System.IO.File.ReadAllText(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "mapskey.txt"));
//                
//                return apiKey;
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine("Failed to parse API Key");
//                return null;
//            }
        }
        //convert to domain object
        public static explicit operator TrafficSettingsViewModel(ViewModels.Traffic tsData)
        {
            TrafficSettingsViewModel trafficSettings = new TrafficSettingsViewModel()
            {
                Id = tsData.Id,
                Enabled = tsData.Enabled,
                Address = tsData.Address,
                WorkAddress = tsData.WorkAddress,
                Driving = tsData.Driving,
                AddressPlaceId = tsData.AddressPlaceId,
                WorkAddressPlaceId = tsData.WorkAddressPlaceId,
                LatLng = tsData.LatLng,
            };

            return trafficSettings;
        }

        ////convert to entity object
        public static explicit operator ViewModels.Traffic(TrafficSettingsViewModel ts)
        {
            ViewModels.Traffic tsData = new ViewModels.Traffic()
            {
                Id = ts.Id,
                Enabled = ts.Enabled,
                Address = ts.Address,
                WorkAddress = ts.WorkAddress,
                Driving = ts.Driving,
                AddressPlaceId = ts.AddressPlaceId,
                WorkAddressPlaceId = ts.WorkAddressPlaceId,
                LatLng = ts.LatLng
            };

            return tsData;
        }
    }
}