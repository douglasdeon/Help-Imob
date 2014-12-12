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
    public partial class Favs : PhoneApplicationPage
    {

        public RSSItem item;
        
        public Favs()
        {
            InitializeComponent();
        }

        private void onClickBrowser(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Browser.xaml?link="+ item.link, UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AtualizarLista();
        }

        private void AtualizarLista()
        {
            List<RSSItem> lista = RSSItemDB.GetRssItem(null);
            ListFavoritos.ItemsSource = lista;
        }

        private void onDelete(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deletar" + item.title + "?", "Atenção", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {

                RSSItemDB.Deletar(item.link);
                AtualizarLista();
            }   
        }

        private void OnSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            item = (sender as ListBox).SelectedItem as RSSItem;
        }
    }
}