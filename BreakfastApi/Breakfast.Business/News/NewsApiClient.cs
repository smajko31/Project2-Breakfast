﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Breakfast.Business.News
{
    public class NewsApiClient
    {
        NewsApiSettings Settings;
        private static readonly string ApiKey = "7d149bd8ce044572abb107044e4abe4a";

        public NewsApiClient()
        {
            Settings = new NewsApiSettings ();
        }

        public NewsApiClient (NewsApiSettings settings)
        {
            this.Settings = settings.Copy();
        }

        public IEnumerable<NewsArticle> GetNewsArticles()
        {
            string json = CallNewsApi();
            return ParseApiRequest(json);
        }

        private IEnumerable<NewsArticle> ParseApiRequest(string json)
        {
            List<NewsArticle> articleList = new List<NewsArticle>();
            int bracketPos = json.IndexOfAny(new char[] { '[' }) + 1;

            string articlesJson;
            do
            {
                Regex articleJsonGrab = new Regex("(?<=\\[)(\\,)?\\{[^\\{\\}]+(\\{[^\\}]+\\})[^\\{\\}]+\\}\\,?");
                articlesJson = articleJsonGrab.Match(json).ToString();
                json = json.Remove(bracketPos, articlesJson.Length);
                if (articlesJson != "")
                {
                    articleList.Add(ParseNewsArticle(articlesJson));
                }
            } while (articlesJson != "");
            return articleList;
        }

        private NewsArticle ParseNewsArticle(string json)
        {
            if (json == "" || json == null)
                return null;
            Regex grabTitle = new Regex("(?<=\"title\":\"){1}([^\"]*)(?=\"){1}");
            Regex grabAuthor = new Regex("(?<=\"author\":\"){1}([^\"]*)(?=\"){1}");
            Regex grabSource = new Regex("(?<=\"name\":\"){1}([^\"]*)(?=\"){1}");
            Regex grabUrl = new Regex("(?<=\"url\":\"){1}([^\"]*)(?=\"){1}");
            Regex grabDesc = new Regex("(?<=\"description\":\"){1}([^\"]*)(?=\"){1}");
            Regex grabDate = new Regex("(?<=\"publishedAt\":\"){1}([^\"]*)(?=\"){1}");

            string title = grabTitle.Match(json).ToString();
            string author = grabAuthor.Match(json).ToString();
            string source = grabSource.Match(json).ToString();
            string url = grabUrl.Match(json).ToString();
            string desc = grabDesc.Match(json).ToString();
            DateTime publDate = DateTime.Parse(grabDate.Match(json).ToString());

            return new NewsArticle(title, author, source, url, desc, publDate);
        }

        public string CallNewsApi()
        {
            //todo:Remove hard coded string and add modular string functions for calling options
            var url = "https://newsapi.org/v2/top-headlines?" +
            GetSubstringDomains() +
            "country=us&" +
            "from=2018-05-17&" +
            "pagesize=24&" +
            "page=1&" +
             GetSubstringApiKey();

            var json = new WebClient().DownloadString(url);
            return json;
        }

        public string GetSubstringLanguage()
        {
            if (Settings.Language == "" || Settings.Language == null)
                return "";
            else
            {
                return "country=" + Settings.Language + "&";
            }
        }

        private string GetSubstringQuery ()
        {
            return null;
        }

        private string GetSubstringSources ()
        {
            if (Settings.Sources[0] == "")
            {
                return "";
            }
            string substring = "sources=";

            for(int i = 0; i < Settings.Sources.Length; i++)
            {
                if(Settings.Sources[i] != "")
                {
                    substring = substring + Settings.Sources[i] + ",";
                }
            }

            substring = substring.Remove(substring.Length - 1, 1) + "&";
            return substring;
        }

        private string GetSubstringDomains ()
        {
            if (Settings.Domains.Count == 0)
            {
                return "";
            }
            else
            {
                string substring = "domains=";
                foreach(string domain in Settings.Domains)
                {
                    substring = substring + "," + domain;
                }
                substring = substring.Remove(substring.Length - 1, 1) + "&";
                return substring;
            }
        }

        private string GetSubstringOldestDate ()
        {
            if (Settings.OldestDate == "")
            {
                return "";
            }
            else
            {

                return null;
            }
        }

        private string GetSubstringNewestDate ()
        {
            if (Settings.Domains.Count == 0)
            {
                return "";
            }
            return null;
        }

        /*private string GetSubstringSortBy ()
        {
            if (Settings.Domains.Count == 0)
            {
                return "";
            }
            return null;
        }*/

        private string GetSubstringPageSize ()
        {
            if (Settings.Domains.Count == 0)
            {
                return "";
            }
            return null;
        }

        private string GetSubstringPage ()
        {
            if (Settings.Domains.Count == 0)
            {
                return "";
            }
            return null;

        }

        private string GetSubstringApiKey()
        {
            return "apiKey=" + ApiKey;
        }
    }
}