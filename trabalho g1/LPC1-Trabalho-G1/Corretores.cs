using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace LPC1_Trabalho_G1
{
  
      [Table(Name = "Corretores")]
    public partial class Corretores
    {      
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column(CanBeNull = false)]
        public String Nome { get; set; }

        [Column(CanBeNull = false)]
        public String email { get; set; }

        [Column(CanBeNull = false)]
        public String fone { get; set; }

        [Column(DbType = "IMAGE", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        public byte[] img { get; set; }
    
        public BitmapImage image { get; set; }
    }
}
