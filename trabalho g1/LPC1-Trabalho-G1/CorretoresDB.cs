using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LPC1_Trabalho_G1
{
    class CorretoresDB
    {
        private static dataBase getDataBasecor()
        {
            dataBase db = new dataBase();
            if (db.DatabaseExists() == false)
            {
                //Cria o banco
                db.CreateDatabase();
            }
            return db;
        }



        //Consulta direta por nome
        public static List<Corretores> GetCorretores(string pNome)
        {
            dataBase db = getDataBasecor();
            var query = from c in db.NomeCor where c.Nome.Contains(pNome) orderby c.Nome select c;

            //where c.Data.Equals(pTipo) orderby c.Data select c;
            List<Corretores> correto = new List<Corretores>(query.AsEnumerable());
            return correto;
        }

    

        public static List<Corretores> GetNotas(string pTipo)
        {
            dataBase db = getDataBasecor();
            var query = from c in db.NomeCor orderby c.Nome select c;

            //where c.Data.Equals(pTipo) orderby c.Data select c;
            List<Corretores> correto = new List<Corretores>(query.AsEnumerable());
            return correto;
        }
        public static Corretores GetCorretor(int pId)
        {
            dataBase db = getDataBasecor();
            Corretores corretores = (from c in db.NomeCor
                                     where c.Id.Equals(pId)
                                     select c).First();

           
            return corretores;
        }



        //Salva uma nota
        public static void Salvar(Corretores nota)
        {
            dataBase db = getDataBasecor();
            db.NomeCor.InsertOnSubmit(nota);
            db.SubmitChanges();
        }

        //Deleta um corretor
        public static void Deletar(int pId)
        {
            dataBase db = getDataBasecor();
            var query = from c in db.NomeCor where c.Id == pId select c;
            db.NomeCor.DeleteOnSubmit(query.ToList()[0]);
            db.SubmitChanges();
        }

        public static void DeletarTudo()
        {
            dataBase db = getDataBasecor();
            db.Nome.DeleteAllOnSubmit(db.Nome);
            db.SubmitChanges();
        }
        
        public static string VerificarCorretor(int pIdCorretor)
       {
           dataBase db = getDataBasecor();

           try
           {
               Corretores corretor = (from t in db.NomeCor where t.Id == pIdCorretor select t).First();

               return "ok";
           }
           catch
           {
               return "vazio";
           }
        }

        public static void Alterar(Corretores pCorretor)
        {
            dataBase db = getDataBasecor();

            Corretores corretorUpdate = (from t in db.NomeCor where t.Id == pCorretor.Id select t).First();

            corretorUpdate.Id = pCorretor.Id;
            corretorUpdate.email = pCorretor.email;
            corretorUpdate.Nome = pCorretor.Nome;
            corretorUpdate.fone = pCorretor.fone;
            corretorUpdate.img = pCorretor.img;
          

            db.SubmitChanges();

        }
    }
}
