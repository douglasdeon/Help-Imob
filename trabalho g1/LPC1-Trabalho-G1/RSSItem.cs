using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPC1_Trabalho_G1
{
    [Table(Name = "RssItem")]
    public class RSSItem
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }

        [Column(CanBeNull = false)]
        public string title { get; set; }
        [Column(CanBeNull = false)]
        public string link { get; set; }
        [Column(CanBeNull = true)]
        public string description { get; set; }
      
        [Column(CanBeNull = true)]
        public string pubDate { get; set; }
        [Column(CanBeNull = true)]
        public string category { get; set; }
    }
}
