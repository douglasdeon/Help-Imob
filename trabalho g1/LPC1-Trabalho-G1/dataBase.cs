using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace LPC1_Trabalho_G1
{
    class dataBase : DataContext
    {
        // String de conexão para o BD
        public static String DBConnectionString = "Data Source = 'isostore:notas.sdf'";

        // Construtor passa a string de conexão para o construtor da classe DataContext
        public dataBase()
            : base(DBConnectionString)
        {
        }

        // Define o mapeamento para notas
        public Table<Casas> Nome
        {
            get
            {
                return this.GetTable<Casas>();
            }
        }

        public Table<Corretores> NomeCor
        {
            get
            {
                return this.GetTable<Corretores>();
            }
        }

        public Table<RSSItem> RSSItem
        {
            get
            {
                return this.GetTable<RSSItem>();
            }
        }
    }
}
