using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPC1_Trabalho_G1
{
    class RSSItemDB
    {
        private static dataBase getDataBase()
        {
            dataBase db = new dataBase();
            if (!db.DatabaseExists())
            {
                db.CreateDatabase();
            }
            return db;
        }

        public static RSSItem RssItem(int pId)
        {
            dataBase db = getDataBase();
            RSSItem itens = (from rss in db.RSSItem where rss.id == pId select rss).First();
            return itens;
        }

        public static List<RSSItem> GetRssItem(string ptitle)
        {
            dataBase db = getDataBase();

            if (ptitle != null)
            {
                var query = from rss in db.RSSItem where rss.title.Contains(ptitle) orderby rss.title select rss;
                List<RSSItem> itens = new List<RSSItem>(query.AsEnumerable());
                return itens;
            }
            else
            {
                var query = from rss in db.RSSItem orderby rss.title select rss;
                List<RSSItem> itens = new List<RSSItem>(query.AsEnumerable());
                return itens;
            }

        }

        public static void Salvar(RSSItem item)
        {
            dataBase db = getDataBase();

            db.RSSItem.InsertOnSubmit(item);

            db.SubmitChanges();
        }

        public static void Deletar(string pLink)
        {
            dataBase db = getDataBase();
            var query = from c in db.RSSItem
                        where c.link == pLink
                        select c;
            db.RSSItem.DeleteOnSubmit(query.ToList()[0]);
            db.SubmitChanges();
        }

        public static string VerifRssItem(string pUrl)
        {
            dataBase db = getDataBase();

            try
            {
                RSSItem favorito = (from t in db.RSSItem where t.link == pUrl select t).First();

                return "ok";
            }
            catch
            {
                return "vazio";
            }
        }
    }
}
