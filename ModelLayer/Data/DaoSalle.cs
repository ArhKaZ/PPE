using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Business;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Data;
using System.Runtime.CompilerServices;
using System.IO;
using CsvHelper;
using System.Globalization;
namespace ModelLayer.Data
{
    public class DaoSalle
    {
        private Dbal myDbal;
        private DaoSalle theDaoSalle;
        private DaoVille theDaoVille;
        private DaoTheme theDaoTheme;
        public DaoSalle(Dbal dbal, DaoVille theDaoVille, DaoTheme theDaoTheme)
        {
            this.myDbal = dbal;
            this.theDaoVille = theDaoVille;
            this.theDaoTheme = theDaoTheme;
        }

        public int ReturnnnextIf()
        {
            DataRow myrow = myDbal.SelectLastId("Salle");
            return (int)myrow["id"] + 1;
        }
        public void Insert(Salle uneSalle)
        {
            string query = "Salle (id, idLieu, idTheme) VALUES ("
                + uneSalle.Id + ","
                + uneSalle.IdLieu.Id + ","
                + uneSalle.IdTheme.Id + ")";
                
            this.myDbal.Insert(query);

        }

        public void Update(Salle uneSalle)
        {
            string query = "Salle Set id= " + uneSalle.Id
                + ", idLieu = " + uneSalle.IdLieu.Id
                + ", idTheme = " + uneSalle.IdTheme.Id;
              
            this.myDbal.Update(query);
        }

        public void Delete(Salle uneSalle)
        {
            string query = "Salle Where id = " + uneSalle.Id;
            this.myDbal.Delete(query);
        }

        public List<Salle> SelectAll()
        {
            List<Salle> listSalle = new List<Salle>();
            DataTable myTable = this.myDbal.SelectAll("Salle");

            foreach (DataRow s in myTable.Rows)
            {
                Ville maVille = this.theDaoVille.SelectbyId((int)s["idLieu"]);
                Theme monTheme = this.theDaoTheme.SelectById((int)s["idTheme"]);
                listSalle.Add(new Salle((int)s["id"], maVille, monTheme));
            }
            return listSalle;
        }

        public Salle SelectById(int id)
        {
            DataRow rowSalle = this.myDbal.SelectById("Salle", id);
            Ville maVille = this.theDaoVille.SelectbyId((int)rowSalle["idLieu"]);
            Theme monTheme = this.theDaoTheme.SelectById((int)rowSalle["idTheme"]);
            return new Salle((int)rowSalle["id"], maVille, monTheme);
        }
    }
}
