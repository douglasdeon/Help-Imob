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
    public partial class visualCorretores : PhoneApplicationPage
    {
        private Corretores correto = new Corretores();
  
        public visualCorretores()
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
            CadastraCorretor();
        }

       

      

        private void AtualizarLista()
        {
            List<Corretores> lista = CorretoresDB.GetNotas(string.Empty);
            lstNotas.ItemsSource = lista;
        }




        private void CadastraCorretor()
        {
            NavigationService.Navigate(new Uri("/cadastraCorretor.xaml", UriKind.Relative));
        }

        private void onClickReiniciar(object sender, EventArgs e)
        {
            CorretoresDB.DeletarTudo();
        }

        private void txb_search_nome(object sender, TextChangedEventArgs e)
        {
            List<Corretores> lista = CorretoresDB.GetCorretores(txSearch.Text);
            lstNotas.ItemsSource = lista;
        }

       

        private void lstCorretor_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            correto = (Corretores)lstNotas.SelectedItem;
            if (lstNotas.SelectedIndex == -1)
                return;
            NavigationService.Navigate(new Uri("/visualizaCorretor.xaml?correto=" + correto.Id, UriKind.Relative));
            lstNotas.SelectedIndex = -1;
        }
    }
}