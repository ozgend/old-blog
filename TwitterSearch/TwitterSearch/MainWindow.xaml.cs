using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reactive;
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

namespace TwitterSearch
{

    public partial class MainWindow : Window
    {
        int pageNo = 1;
        string keyword = string.Empty;
        ObservableCollection<TwitterSearchResult.Tweet> listSource = new ObservableCollection<TwitterSearchResult.Tweet>();

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
            listResult.ItemsSource = listSource;

            //event TextBox.TextChanged
            var search = Observable
                .FromEventPattern<TextChangedEventArgs>(txtKeyword, "TextChanged")
                .Select(evt => ((TextBox)evt.Sender).Text)
                .Where(txt => txt.Length > 3)
                .Throttle(TimeSpan.FromMilliseconds(400))
                .Select(txt =>
                {
                    //download current keyword
                    keyword = txt;
                    return Helper.Download(keyword, pageNo);
                })
                .ObserveOnDispatcher()
                .Subscribe(result =>
                {
                    listSource.Clear();
                    result.results.ForEach(t => listSource.Add(t));
                });

            //event ListBox.ScrollBar.Scroll
            ScrollBar scrollBar = Helper.FindVisualChild<ScrollBar>(listResult);
            var paging = Observable
                .FromEventPattern<ScrollEventArgs>(scrollBar, "Scroll")
                .Select(evt => ((ScrollBar)evt.Sender).Value)
                .Where(val => val == scrollBar.Maximum)
                .Throttle(TimeSpan.FromMilliseconds(400))
                .Select(val =>
                {
                    //download next page
                    return Helper.Download(keyword, pageNo + 1);
                })
                .ObserveOnDispatcher()
                .Subscribe(result =>
                {
                    result.results.ForEach(t => listSource.Add(t));
                    pageNo++;
                });

        }

    }
}
