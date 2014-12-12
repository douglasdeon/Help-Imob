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
    public partial class visualizaCasa : PhoneApplicationPage
    {
        private Casas casa = new Casas();
        int idant;
        public visualizaCasa()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var data = this.NavigationContext.QueryString;
            if (data.ContainsKey("idCasa"))
            {
                
                idant = Convert.ToInt32(data["idCasa"].ToString());
                Casas list = CasasDB.GetCasa(idant);

                txtNomeCor.Text = list.nomeCorretor;
                txtEndereco.Text = list.endereco;
                txtDescricao.Text = list.Descricao;
                txtTitulo.Text = list.titulo;
                BitmapImage imag = reConveter(list);


                imgCaptura.Source = imag;


            }
            base.OnNavigatedTo(e);
        }
        private BitmapImage reConveter(Casas obj)
        {
            BitmapImage imag = new BitmapImage();

            if (obj.img != null && obj.img is byte[])
            {
                byte[] bytes = obj.img as byte[];
                MemoryStream stream = new MemoryStream(bytes);

                imag.SetSource(stream);
                obj.image = imag;
            }
            return imag;
        }

        private void OnClickDeletar(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Deletar Casa: " + casa.titulo + "?", "Atenção", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                CasasDB.Deletar(idant);
                NavigationService.GoBack();
            }
        
        }

        

        private void OnClickEditar(object sender, EventArgs e)
        {
            
            NavigationService.Navigate(new Uri("/cadastraCasa.xaml?idCasa=" + idant, UriKind.Relative));
          
        }
    }
}