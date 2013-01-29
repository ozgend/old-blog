using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TwitterSearchBegin
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DockPanel_Loaded_1(object sender, RoutedEventArgs e)
        {
            Bind();
        }

        private void Bind()
        {

            txtKeyword.Focus();

            ObservableCollection<string> listSource = new ObservableCollection<string>();
            listResult.ItemsSource = listSource;

            int _pageNo = 1;
            string _keyword = string.Empty;

            var search = Observable.FromEventPattern<TextChangedEventArgs>(txtKeyword, "TextChanged")
                .Select(_event => (_event.Sender as TextBox).Text)
                .Throttle(TimeSpan.FromMilliseconds(600))
                .Where(keyword => keyword.Length > 0)
                .Select(keyword =>
                {
                    _keyword = keyword;
                    return Download(keyword, _pageNo);
                })
                .ObserveOnDispatcher()
                .Subscribe(result =>
                {
                    listSource.Clear();
                    result.results.ForEach(tweet => listSource.Add(tweet.ToString()));
                });


            ScrollBar scrollBar = FindVisualChild<ScrollBar>(listResult);
            var paging = Observable.FromEventPattern<ScrollEventArgs>(scrollBar, "Scroll")
                .Select(evt => ((ScrollBar)evt.Sender).Value)
                .Where(val => val == scrollBar.Maximum)
                .Throttle(TimeSpan.FromMilliseconds(400))
                .Select(val => { return Download(_keyword, _pageNo + 1); })
                .ObserveOnDispatcher()
                .Subscribe(result =>
                {
                    result.results.ForEach(tweet => listSource.Add(tweet.ToString()));
                    _pageNo++;
                });
        }


        public TwitterSearchResult Download(string keyword, int pageNo)
        {
            string url = string.Format("http://search.twitter.com/search.json?q={0}&page={1}", keyword, pageNo);

            using (WebClient w = new WebClient())
            {
                string json = w.DownloadString(url);
                return JsonConvert.DeserializeObject<TwitterSearchResult>(json);
            }
        }

        public class TwitterSearchResult
        {
            public List<Tweet> results { get; set; }

            public class Tweet
            {
                public string created_at { get; set; }
                public string from_user { get; set; }
                public string profile_image_url { get; set; }
                public string text { get; set; }

                public string ToString()
                {
                    return string.Format("@{0} {1}", this.from_user, this.text);
                }
            }
        }

        public T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
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
