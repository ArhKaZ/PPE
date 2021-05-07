using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Business;
using System.Data;
using System.Runtime.CompilerServices;
using System.IO;
using System.Globalization;

namespace ModelLayer.Data
{
    public class DaoVille
    {
        private Dbal mydbal;
       

        public DaoVille(Dbal dbal)
        {
            mydbal = dbal;
        }

        public void Insert(Ville uneVille)
        {
            string query = "Ville (id, nom) VALUES ("
                + uneVille.Id + ",'"
                + uneVille.Nom.Replace("'", "''");
            this.mydbal.Insert(query);
        }

        public void Update(Ville uneVille)
        {
            string query = "Ville Set id = " + uneVille.Id
                + ", nom = '" + uneVille.Nom.Replace("'", "''")
                + "Where id = " + uneVille.Id;
            this.mydbal.Update(query);
        }

        public void Delete(Ville uneVille)
        {
            string query = "Ville Where id = " + uneVille.Id;
            this.mydbal.Delete(query);
        }

        public List<Ville> SelectAll()
        {
            List<Ville> listVille = new List<Ville>();
            DataTable myTable = this.mydbal.SelectAll("Ville");

            foreach (DataRow r in myTable.Rows)
            {
                listVille.Add(new Ville((int)r["id"], (string)r["nom"]));
            }
            return listVille;
        }

        public Ville SelectbyId(int id)
        {
            DataRow rowVille = this.mydbal.SelectById("Ville", id);
            return new Ville((int)rowVille["id"],(string)rowVille["nom"]);
        }

       
    }
}
