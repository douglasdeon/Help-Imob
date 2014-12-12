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
using Microsoft.Phone.Tasks;
using System.Windows.Resources;
using System.IO;

namespace LPC1_Trabalho_G1
{
    public partial class cadastraNota : PhoneApplicationPage
    {

        private CameraCaptureTask camera { get; set; }
        private string caminho { get; set; }
        private byte[] imageBytes { get; set; }
        int idant = 0;

        public cadastraNota()
        {
            InitializeComponent();
        }



        private void Cadastrar_Click(object sender, EventArgs e)
        {
            Casas c = new Casas();
            if (idant == 0)
            {
                c.endereco = txbNome.Text;
                c.Descricao = txbDesc.Text;
                c.titulo = txbTitulo.Text;
                c.Data = DateTime.Now;
                c.img = imageBytes;
                if (txbIdCorretor.Text == string.Empty)
                {
                    MessageBox.Show("Corretor não selecionado");
                }
                else
                {
                    c.idCorretor = Convert.ToInt32(txbIdCorretor.Text);

                    string resultado = CorretoresDB.VerificarCorretor(c.idCorretor);

                    if (resultado == "ok")
                    {
                        CasasDB.Salvar(c);
                        MessageBox.Show(c.titulo + " salvo com sucesso.");
                        NavigationService.GoBack();
                    }
                    else
                    {
                        MessageBox.Show("Corretor Inexistente");
                    }


                }
            }
            else
            {
                c.Id = idant;
                c.endereco = txbNome.Text;
                c.Descricao = txbDesc.Text;
                c.titulo = txbTitulo.Text;
                c.Data = DateTime.Now;
                c.img = imageBytes;
                if (txbIdCorretor.Text == string.Empty)
                {
                    MessageBox.Show("Corretor não selecionado");
                }
                else
                {
                    c.idCorretor = Convert.ToInt32(txbIdCorretor.Text);
                    string resultado = CorretoresDB.VerificarCorretor(c.idCorretor);

                    if (resultado == "ok")
                    {
                        CasasDB.Alterar(c);
                        MessageBox.Show(c.titulo + " salvo com sucesso.");
                        NavigationService.GoBack();
                    }
                    else
                    {
                        MessageBox.Show("Corretor Inexistente");
                    }


                }
            }
           

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            var data = this.NavigationContext.QueryString;

            if (data.ContainsKey("idCasa"))
            {
                
                idant = Convert.ToInt32(data["idCasa"].ToString());
                Casas list = CasasDB.GetCasa(idant);
                txbNome.Text = list.endereco;
                txbDesc.Text = list.Descricao;
                txbTitulo.Text = list.titulo;
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

        private void Cadastrar_Photo(object sender, EventArgs e)
        {
            camera = new CameraCaptureTask();
            camera.Completed += camera_Completed;
            camera.Show();
        }




        void camera_Completed(object sender, PhotoResult e)
        {
            BitmapImage imgTemp = new BitmapImage();
            caminho = e.OriginalFileName;
            imgTemp.SetSource(e.ChosenPhoto);
            imgCaptura.Source = imgTemp;
         
            imageBytes = ConvertToBytes(imgTemp);
        }

        public static byte[] ConvertToBytes(BitmapImage bitmapImage)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                var wBitmap = new WriteableBitmap(bitmapImage);
                wBitmap.SaveJpeg(stream, wBitmap.PixelWidth, wBitmap.PixelHeight, 0, 100);
                stream.Seek(0, SeekOrigin.Begin);
                return stream.ToArray();
            }
        }
    


    }
}
