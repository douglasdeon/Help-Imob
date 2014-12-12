using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LPC1_Trabalho_G1
{
    class CasasDB
    {
        //Cria um objeto dataBase. Se for necessário cria o banco de dados
        private static dataBase getDataBase()
        {
            dataBase db = new dataBase();
            if (db.DatabaseExists() == false)
            {
                //Cria o banco
                db.CreateDatabase();
            }
            return db;
        }



        //Consulta direta por tipo
        public static List<Casas> GetNotas(string pTipo)
        {
            dataBase db = getDataBase();
            var query = from c in db.Nome orderby c.Data select c;

            //where c.Data.Equals(pTipo) orderby c.Data select c;
            List<Casas> casas = new List<Casas>(query.AsEnumerable());
            return casas;
        }

        public static Casas GetCasa(int pId)
        {
            dataBase db = getDataBase();
            Casas casa = new Casas();
            var result = from t in db.Nome
                         where t.Id == pId
                          join a in db.NomeCor on t.idCorretor equals a.Id
                         
                          select new
                          {
                              Id = t.Id,
                              Titulo = t.titulo,
                              Descricao = t.Descricao,
                              Endereco = t.endereco,
                              Nome = a.Nome,
                              Imagem = t.img

                          };
            foreach (var item in result)
            {
                casa.Id = item.Id;
                casa.endereco = item.Endereco;
                casa.Descricao = item.Descricao;
                casa.img = item.Imagem;
                casa.nomeCorretor = item.Nome;
                casa.titulo = item.Titulo;
            }
            return casa;
        }

        public static List<Casas> GetCasa(string pNome)
        {
            dataBase db = getDataBase();
            var query = from c in db.Nome where c.endereco.Contains(pNome) orderby c.endereco select c;

            List<Casas> casas = new List<Casas>(query.AsEnumerable());
            return casas;
        }

    
        public static void Salvar(Casas nota)
        {
            dataBase db = getDataBase();
            db.Nome.InsertOnSubmit(nota);
            db.SubmitChanges();
        }

        //Deleta uma nota
        public static void Deletar(int pId)
        {
            dataBase db = getDataBase();
            var query = from c in db.Nome where c.Id == pId select c;

            db.Nome.DeleteOnSubmit(query.ToList()[0]);
            db.SubmitChanges();
        }

        public static void DeletarTudo()
        {
            dataBase db = getDataBase();
            db.Nome.DeleteAllOnSubmit(db.Nome);
            db.SubmitChanges();
        }

        public static void Alterar(Casas pCasa)
        {
            dataBase db = getDataBase();

            Casas casaUpdate = (from t in db.Nome where t.Id == pCasa.Id select t).First();

            casaUpdate.Id = pCasa.Id;
            casaUpdate.endereco = pCasa.endereco;
            casaUpdate.Descricao = pCasa.Descricao;
            casaUpdate.titulo = pCasa.titulo;
            casaUpdate.img = pCasa.img;
            casaUpdate.idCorretor = pCasa.idCorretor;

            db.SubmitChanges();
         
        }
   
    }
}
