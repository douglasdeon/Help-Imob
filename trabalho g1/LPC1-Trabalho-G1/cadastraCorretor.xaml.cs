using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace LPC1_Trabalho_G1
{
    public partial class cadastraCorretor : PhoneApplicationPage
    {
        private CameraCaptureTask camera { get; set; }
        private string caminho { get; set; }
        private byte[] imageBytes { get; set; }
        int idant = 0;
        public cadastraCorretor()
        {
            InitializeComponent();
        }

        private void Cadastrar_Click(object sender, EventArgs e)
        {
            Corretores c = new Corretores();

            if (idant == 0)
            {

                c.Nome = txbNome.Text;
                c.email = txbEmail.Text;
                c.fone = txbFone.Text;
                c.img = imageBytes;

                CorretoresDB.Salvar(c);
                MessageBox.Show(c.Nome + " salvo com sucesso.");

                NavigationService.GoBack();
            }
            else
            {
                c.Id = idant;
                c.Nome = txbNome.Text;
                c.email = txbEmail.Text;
                c.fone = txbFone.Text;  
             
                c.img = imageBytes;

                CorretoresDB.Alterar(c);
                MessageBox.Show(c.Nome + " salvo com sucesso.");

                NavigationService.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var data = this.NavigationContext.QueryString;
            if (data.ContainsKey("correto"))
            {
                
                idant = Convert.ToInt32(data["correto"].ToString());
                Corretores list = CorretoresDB.GetCorretor(idant);
                txbNome.Text = list.Nome;
                txbEmail.Text = list.email;
                txbFone.Text = list.fone;
                BitmapImage imag = reConveter(list);
                

                imgCaptura.Source = imag;

            }
            base.OnNavigatedTo(e);
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
       }
    }
