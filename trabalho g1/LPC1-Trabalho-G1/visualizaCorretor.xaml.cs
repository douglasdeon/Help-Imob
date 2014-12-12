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
    public partial class visualizaCorretor : PhoneApplicationPage
    {
        private Corretores corretor = new Corretores();
        int idant;
        public visualizaCorretor()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var data = this.NavigationContext.QueryString;
            if (data.ContainsKey("correto"))
            {

                idant = Convert.ToInt32(data["correto"].ToString());
                corretor = CorretoresDB.GetCorretor(idant);

                txtNome.Text = corretor.Nome;
                txtFone.Text = corretor.fone;
                txtMail.Text = corretor.email;

                BitmapImage imag = reConveter(corretor);


                imgCaptura.Source = imag;


            }
            base.OnNavigatedTo(e);
        }
        private BitmapImage reConveter(Corretores obj)
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
            if (MessageBox.Show("Deletar Corretor: " + corretor.Nome + "?", "Atenção", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                CorretoresDB.Deletar(idant);
                NavigationService.GoBack();
            }
        }

        private void OnClickEditar(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/cadastraCorretor.xaml?correto=" + idant, UriKind.Relative));
        }
    }
}