using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace LPC1_Trabalho_G1
{
    public partial class Browser : PhoneApplicationPage
    {

        string link;
        string title;
        public Browser()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {



            url.Navigated += new EventHandler<System.Windows.Navigation.NavigationEventArgs>(url_Navigated);
            url.Navigating += new EventHandler<NavigatingEventArgs>(url_Navigating);
            url.ScriptNotify += new EventHandler<NotifyEventArgs>(url_ScriptNotify);

          
            if (NavigationContext.QueryString.TryGetValue("link", out link))
            {
                
                url.Navigate(new Uri(link, UriKind.Absolute));
                url.IsScriptEnabled = true;
            }
            if (NavigationContext.QueryString.TryGetValue("title", out title))
            {
                title = title;
            }

        }

        void url_Navigating(object sender, NavigatingEventArgs e)
        {
            progress.Visibility = System.Windows.Visibility.Visible;
        }

        private void url_Navigated(object sender, NavigationEventArgs e)
        {
            progress.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void url_ScriptNotify(object sender, NotifyEventArgs e)
        {
            url.Navigate(new Uri(e.Value, UriKind.Absolute));
        }

        private void RadioButton_Checked_2(object sender, EventArgs e)
        {
            string result = RSSItemDB.VerifRssItem(link);
            if (result.Equals("vazio"))
            {

                RSSItem favorito = new RSSItem();
                favorito.link = link;
                favorito.title = title;

                RSSItemDB.Salvar(favorito);
                MessageBox.Show("Notícia salva com sucesso!!");
            }
            else
            {
                RSSItemDB.Deletar(link);
                MessageBox.Show("Notícia Deletada");
            }
        }

    
    }
}