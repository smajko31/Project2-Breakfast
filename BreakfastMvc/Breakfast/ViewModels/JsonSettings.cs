﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Breakfast.ViewModels
{
    //Generated by json2csharp.com, based on sample json response
    public class JsonSettings
    {
        public class Weather
        {
            public int id { get; set; }
            public bool enabled { get; set; }
            public bool farenheit { get; set; }
            public bool windSpeed { get; set; }
            public bool humidity { get; set; }
            public bool cloudiness { get; set; }
            public string location { get; set; }
        }

        public class Traffic
        {
            public int Id { get; set; }
            public bool Enabled { get; set; }
            public object Address { get; set; }
            public object WorkAddress { get; set; }
            public bool Driving { get; set; }
            public object AddressPlaceId { get; set; }
            public object WorkAddressPlaceId { get; set; }
            public object LatLng { get; set; }
        }

        public class News
        {
            public int Id { get; set; }
            public List<object> Queries { get; set; }
            public List<object> Sources { get; set; }
            public List<object> Domains { get; set; }
            public object Language { get; set; }
            public object PageSize { get; set; }
            public string OldestDate { get; set; }
            public string NewestDate { get; set; }
        }

        public class RootObject
        {
            public string UserEmail { get; set; }
            public Weather Weather { get; set; }
            public Traffic Traffic { get; set; }
            public News News { get; set; }
        }
    }
}