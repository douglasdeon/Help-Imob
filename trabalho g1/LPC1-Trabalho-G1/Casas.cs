using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Windows.Resources;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace LPC1_Trabalho_G1
{
    [Table(Name = "Casas")]
    public class Casas
    {
        public string nomeCorretor { get; set; }


        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column(CanBeNull = false)]
        public int idCorretor { get; set; }

        [Column(CanBeNull = false)]
        public String titulo { get; set; }

        [Column(CanBeNull = false)]
        public String endereco { get; set; }

        [Column(CanBeNull = false)]
        public String Descricao { get; set; }

        [Column(CanBeNull = false)]
        public DateTime Data { get; set; }

        [Column(DbType = "IMAGE", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        public byte[] img { get; set; }
        public BitmapImage image { get; set; }

        
       
     
    }
}
