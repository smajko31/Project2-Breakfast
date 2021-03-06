﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Breakfast.Business.News.Models;
using Breakfast.Data;

namespace Breakfast.Business.News
{
    public class NewsCrud
    {
        public static void SaveSettings(string userId, NewsSettings ns)
        {
            Storage storage = new Storage(new DefaultDBUtils());
            ns.Id = storage.GetNewsId(userId);
            Data.Models.NewsSettings nsData = (Data.Models.NewsSettings) ns ;
            storage.SaveNewsSettings( nsData);
        }

        public static NewsSettings ReadSettings(string userId)
        {
            Storage storage = new Storage(new DefaultDBUtils());
            return (NewsSettings) storage.GetNewsSettings(userId);
        }
    }
}
