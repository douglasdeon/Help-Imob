using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using System.IO;

namespace LPC1_Trabalho_G1
{
    public partial class casaXangrila : PhoneApplicationPage
    {
        private Casas casa = new Casas();
        public casaXangrila()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            AtualizarLista();
        }

        private void onClickNovo(object sender, EventArgs e)
        {
            CadastraCasa();
        }

       

     

        private void AtualizarLista()
        {
            List<Casas> lista = CasasDB.GetNotas(string.Empty);
            
            lstNotas.ItemsSource = lista;
        }




       
        

        private void CadastraCasa()
        {
            NavigationService.Navigate(new Uri("/cadastraCasa.xaml", UriKind.Relative));
        }

     

        private void txb_search_nome(object sender, TextChangedEventArgs e)
        {
            List<Casas> lista = CasasDB.GetCasa(txSearch.Text);
            lstNotas.ItemsSource = lista;
        }

        private void lstCasa_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            casa = (Casas)lstNotas.SelectedItem;
            if (lstNotas.SelectedIndex == -1)
                return;

            NavigationService.Navigate(new Uri("/visualizaCasa.xaml?idCasa=" + casa.Id, UriKind.Relative));
            lstNotas.SelectedIndex = -1;
        }
    }
}