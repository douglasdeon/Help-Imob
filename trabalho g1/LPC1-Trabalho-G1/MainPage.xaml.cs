using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Xml.Linq;

namespace LPC1_Trabalho_G1
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Casas nota = new Casas();
        private Corretores correto = new Corretores();

        public RSSItem item;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            WebClient rssCliente = new WebClient();
            rssCliente.DownloadStringCompleted += new DownloadStringCompletedEventHandler(rssCliente_DownloadStringCompleted);

            rssCliente.Encoding = System.Text.Encoding.GetEncoding("utf-8");

            rssCliente.DownloadStringAsync(new Uri(@"http://feeds.feedburner.com/EXAME-imoveis?format=xml"));
        }

        void rssCliente_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var rssData = from rss in XElement.Parse(e.Result).Descendants("item")
                          select new RSSItem
                          {
                              title = rss.Element("title").Value,
                              pubDate = rss.Element("pubDate").Value,
                              category = rss.Element("category").Value,
                              description = rss.Element("description").Value,
                              link = rss.Element("link").Value
                          };
            lstRSS.ItemsSource = rssData;
            progress.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void lstRSS_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            item = (sender as ListBox).SelectedItem as RSSItem;

            if (lstRSS.SelectedIndex == -1)
                return;

            NavigationService.Navigate(new Uri("/Browser.xaml?link=" + item.link +"&title=" +item.title, UriKind.Relative));

            lstRSS.SelectedIndex = -1;
        }

      
     

        private void onClickNovo(object sender, EventArgs e)
        {
            CadastraCasa();
        }


     


        private void CadastraCasa()
        {
            NavigationService.Navigate(new Uri("/cadastraCasa.xaml", UriKind.Relative));
        }

  
        private void Corretores()
        {
            try
            {
                NavigationService.Navigate(new Uri("/visualCorretores.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

      

     

       
 

       

        private void onCLickCorretores(object sender, RoutedEventArgs e)
        {
            Corretores();
        }

  

        private void RadioButton_Checked_2(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Favs.xaml", UriKind.Relative));
        }

        private void onCLickCasas(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/casaXangrila.xaml", UriKind.Relative));
        }
    }
}