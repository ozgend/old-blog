using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TwitterSearch
{
    public static class Helper
    {

        const string query = "http://search.twitter.com/search.json?q={0}&page={1}";

        public static TwitterSearchResult Download(string keyword, int pageNo)
        {
            string url = string.Format(query, keyword, pageNo);
            Debug.WriteLine(string.Format("++DownloadString = {0}", url));

            using (WebClient w = new WebClient())
            {

                string json = w.DownloadString(url);
                return JsonConvert.DeserializeObject<TwitterSearchResult>(json);
            }
        }

        public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }

    }
}
