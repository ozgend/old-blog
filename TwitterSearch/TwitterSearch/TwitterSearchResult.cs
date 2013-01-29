using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterSearch
{
    public class TwitterSearchResult
    {
        public TwitterSearchResult()
        {
            this.results = new List<Tweet>();
        }

        public double completed_in { get; set; }
        public long max_id { get; set; }
        public string max_id_str { get; set; }
        public string next_page { get; set; }
        public int page { get; set; }
        public string query { get; set; }
        public string refresh_url { get; set; }
        public List<Tweet> results { get; set; }
        public int results_per_page { get; set; }
        public int since_id { get; set; }
        public string since_id_str { get; set; }

        public class Tweet
        {
            public string created_at { get; set; }
            public string from_user { get; set; }
            public int from_user_id { get; set; }
            public string from_user_id_str { get; set; }
            public string from_user_name { get; set; }
            public object geo { get; set; }
            public object id { get; set; }
            public string id_str { get; set; }
            public string iso_language_code { get; set; }
            public string profile_image_url { get; set; }
            public string profile_image_url_https { get; set; }
            public string source { get; set; }
            public string text { get; set; }
            public string to_user { get; set; }
            public int to_user_id { get; set; }
            public string to_user_id_str { get; set; }
            public string to_user_name { get; set; }
            public long? in_reply_to_status_id { get; set; }
            public string in_reply_to_status_id_str { get; set; }

            public override string ToString()
            {
                return string.Format("@{0}\n  {1}",this.from_user,this.text);
            }
        }      

    }
}
