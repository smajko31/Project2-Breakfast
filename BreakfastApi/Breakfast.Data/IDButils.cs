﻿using Breakfast.Data.Models;

namespace Breakfast.Data
{
    public interface IDButils
    {
        // Initialize settings for new accounts
        void InitializeSettings(SettingsTable st);

        // Get settings on login
        SettingsTable GetSettings(string userId);

        // Get individual settings id
        int GetNewsId(string userId);
        int GetTrafficId(string userId);
        int GetWeatherId(string userId);

        // Save settings
        void SaveNewsSettings(NewsSettings ns);
        void SaveTrafficSettings(TrafficSettings ts);
        void SaveWeatherSettings(WeatherSettings ws);
    }
}
