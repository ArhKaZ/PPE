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
    public class DaoTheme
    {
        private Dbal mydbal;
        private DaoTheme theDaoTheme;

        public DaoTheme(Dbal dbal)
        {
            mydbal = dbal;
           
        }

        public void Insert(Theme unTheme)
        {
            string query = "Theme (id, nom) VALUES ("
                + unTheme.Id + ",'"
                + unTheme.Nom.Replace("'","''") + "')";
                
            this.mydbal.Insert(query);

        }

        public void Update(Theme unTheme)
        {
            string query = "Utilisateur Set id= " + unTheme.Id
                + ", nom = '" + unTheme.Nom.Replace("'", "''") + ")";
            this.mydbal.Update(query);
        }

        public void Delete(Theme unTheme)
        {
            string query = "Theme Where id = " + unTheme.Id;
            this.mydbal.Delete(query);
        }

        public List<Theme> SelectAll()
        {
            List<Theme> listTheme = new List<Theme>();
            DataTable myTable = this.mydbal.SelectAll("Theme");

            foreach (DataRow t in myTable.Rows)
            {
                listTheme.Add(new Theme((int)t["id"], (string)t["nom"]));
            }
            return listTheme;
        }
        
        public Theme SelectById(int id)
        {
            DataRow rowTheme = this.mydbal.SelectById("theme", id);
            return new Theme((int)rowTheme["id"], (string)rowTheme["nom"]);
        }
    }
}
