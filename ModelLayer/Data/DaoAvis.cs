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
    public class DaoAvis
    {
        private Dbal mydbal;
        private DaoAvis theDaoAvis;

        private DaoClient theDaoCLient;
        private DaoTheme theDaoTheme;

        public DaoAvis(Dbal mydbal, DaoClient theDaoCLient, DaoTheme theDaoTheme)
        {
            this.mydbal = mydbal;
            this.theDaoCLient = theDaoCLient;
            this.theDaoTheme = theDaoTheme;
        }


        public void Insert(Avis theAvis, Client theClient, Theme theTheme)
        {
            string query = "Avis(id, idClient, note, commentaire, idTheme) VALUES ("
                + theAvis.Id + ","
                + theClient.Id + ","
                + theAvis.Note + ","
                + theAvis.Commentaire + ","
                + theTheme.Id + "')";
            this.mydbal.Insert(query);
        }
        //en commentaire le insertFromCSV
        /*

        public void InsertFromCSV(string filename)
        {
            using (var reader = new StreamReader(filename))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();

                var record = new Avis();
                var records = csv.EnumerateRecords(record);

                foreach (Avis r in records)
                {
                    Console.WriteLine(r.Id + "---" + r.Note);
                    this.Insert(record);
                }
            }
        }

        */


        public void Update(Avis myAvis, Client myCLient, Theme myTheme)
        {
            string query = "Avis SET id = " + myAvis.Id
                + ", idClient = " + myCLient.Id
                + ", note =" + myAvis.Note
                + ", commentaire = '" + myAvis.Commentaire.Replace("'", "''")
                + "', idTheme = " + myTheme.Id

                + "where id = " + myAvis.Id;
            this.mydbal.Update(query);
        }


        public List<Avis> SelectAll()
        {
            List<Avis> listAvis = new List<Avis>();
            DataTable myTable = this.mydbal.SelectAll("Avis");

            foreach (DataRow r in myTable.Rows)
            {

                Client myClient = this.theDaoCLient.SelectById((int)r["id"]);
                Theme myTheme = this.theDaoTheme.SelectById((int)r["id"]);
                listAvis.Add(new Avis((int)r["id"], myClient, (int)r["note"], (string)r["commentaire"], myTheme));

            }
            return listAvis;
        }

        public Avis SelectById(int id)
        {
            DataRow rowAvis = this.mydbal.SelectById("Avis", id);

            Client myCLient = this.theDaoCLient.SelectById((int)rowAvis["id"]);
            Theme myTheme = this.theDaoTheme.SelectById((int)rowAvis["id"]);
            Avis myAvis = this.theDaoAvis.SelectById((int)rowAvis["idClient"]);
            return new Avis((int)rowAvis["id"], myCLient, (int)rowAvis["note"], (string)rowAvis["commentaire"], myTheme);

        }

        public void Delete(Avis unAvis)
        {
            string query = " Avis where id = " + unAvis.Id + ";";

            this.mydbal.Delete(query);
        }
    }
}
