using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using ModelLayer.Business;

namespace ModelLayer.Data
{
    public class DaoObstacle
    {
        private Dbal mydbal;
        private DaoObstacle theDaoObstacle;
        private DaoTheme theDaoTheme;


        public DaoObstacle(Dbal dbal, DaoTheme theDaoTheme)
        {
            this.mydbal = dbal;
            this.theDaoTheme = theDaoTheme;
        }

        public void Insert(Obstacle theObstacle, Theme theTheme)

        {
            string query = "Obstacle(id, nom, photo, commentaire, difficulte, prix, theme) VALUES ("
                + theObstacle.Id + ",'"
                + theObstacle.Nom + "','"
                + theObstacle.Photo + "','"
                + theObstacle.Commentaire + "',"
                + theObstacle.Difficulte + ",'"
                + theObstacle.Prix + ",'"

                + theTheme.Id + ")";
            this.mydbal.Insert(query);
        }
        /*

        public void InsertFromCSV(string filename)
        {
            using (var reader = new StreamReader(filename))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();

                var record = new Obstacle();
                var records = csv.EnumerateRecords(record);

                foreach (Obstacle r in records)
                {
                    Console.WriteLine(r.Id + "-" + r.Nom);
                    this.Insert(record);
                }
            }
        }

        */

        public void Update(Obstacle myObstacle, Theme myTheme)

        {
            string query = "Obstacle SET id= "
                + myObstacle.Id
                + ", nom = '" + myObstacle.Nom
                + "', photo = '" + myObstacle.Photo
                + "', commentaire = '" + myObstacle.Commentaire
                + "', difficulte = " + myObstacle.Difficulte
                + ", prix = " + myObstacle.Prix

                + ", theme = " + myTheme.Id;


            this.mydbal.Update(query);

        }

        public List<Obstacle> SelectAll()
        {
            List<Obstacle> listObstacle = new List<Obstacle>();
            DataTable myTable = this.mydbal.SelectAll("Obstacle");

            foreach (DataRow r in myTable.Rows)
            {

                Theme myTheme = this.theDaoTheme.SelectById((int)r["id"]);
                listObstacle.Add(new Obstacle((int)r["id"], (string)r["nom"], (string)r["photo"], (string)r["commentaire"], (int)r["difficulte"], (int)r["prix"], myTheme));


            }
            return listObstacle;
        }

        public Obstacle SelectById(int id)
        {
            DataRow rowObstacle = this.mydbal.SelectById("Obstacle", id);

            Theme myTheme = this.theDaoTheme.SelectById((int)rowObstacle["id"]);
            return new Obstacle((int)rowObstacle["id"], (string)rowObstacle["nom"], (string)rowObstacle["photo"], (string)rowObstacle["commentaire"], (int)rowObstacle["difficulte"], (int)rowObstacle["prix"], myTheme);


        }

        public Obstacle SelectByName(string name)
        {
            string search = "nom = '" + name + "'";
            DataTable tableObstacle = this.mydbal.SelectByField("Obstacle", search);

            Theme myTheme = this.theDaoTheme.SelectById((int)tableObstacle.Rows[0]["id"]);

            return new Obstacle((int)tableObstacle.Rows[0]["id"], (string)tableObstacle.Rows[0]["nom"], (string)tableObstacle.Rows[0]["photo"], (string)tableObstacle.Rows[0]["commentaire"], (int)tableObstacle.Rows[0]["difficulte"], (int)tableObstacle.Rows[0]["prix"], (Theme)tableObstacle.Rows[0]["theme"]);

        }

        public void Delete(Obstacle unObstacle)
        {
            string query = "Obstacle where id = " + unObstacle.Id + ";";
            this.mydbal.Delete(query);
        }
    }
}
