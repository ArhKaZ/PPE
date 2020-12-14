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
    public class DaoClient
    {

        private Dbal theDbal;

        public DaoClient(Dbal dbal)
        {

            this.theDbal = dbal;

        }
        public int ReturnnextId()
        {
            DataRow myRow = theDbal.SelectLastId("Clients");
            return (int)myRow["id"] + 1;
        }

        public List<Client> SearchbyName(string table, string conditions)
        {
            List<Client> listClient = new List<Client>();
            DataTable myTable = theDbal.SelectByField(table, conditions);
            foreach (DataRow r in myTable.Rows)
            {
                listClient.Add(new Client((int)r["id"], (string)r["nom"], (string)r["prenom"], (int)r["telephone"], (string)r["mail"], (int)r["credit"], (DateTime)r["dateNaissance"], (int)r["NbPartie"]));
            }
            return listClient;
        }
        public void Insert(Client theClient)
        {
            int id = ReturnnextId();
            


            string query = "Clients(id, nom, prenom, telephone, mail, credit, dateNaissance, NbPartie) VALUES ("
                + id + ",'"
                + theClient.Nom + "','"
                + theClient.Prenom + "',"
                + theClient.Telephone + ",'"
                + theClient.Mail + "',"
                + theClient.Credit + ",'"
                + theClient.DateNaissance.ToString("yyyy-MM-dd") + "',"
                + theClient.Nbpartie + ")";

            this.theDbal.Insert(query);

        }

        public void InsertFromCSV(string filename)
        {
            using (var reader = new StreamReader(filename))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();

                var record = new Client();
                var records = csv.EnumerateRecords(record);

                foreach (Client r in records)

                {

                    Console.WriteLine(r.Id + "-" + r.Nom);
                    this.Insert(record);
                }
            }
        }

        public void Update(Client myCLient)
        {
            string query = "Clients SET"
            + " nom = '" + myCLient.Nom
            + "', prenom = '" + myCLient.Prenom
            + "', telephone =" + myCLient.Telephone
            + ", mail = '" + myCLient.Mail
            + "', credit = " + myCLient.Credit
            + ", dateNaissance = '" + myCLient.DateNaissance.ToString("yyyy-MM-dd")
            + "', NbPartie =" + myCLient.Nbpartie
            + " Where id = " + myCLient.Id;
            this.theDbal.Update(query);

        }


        public List<Client> SelectAll()
        {
            List<Client> listClient = new List<Client>();

            DataTable myTable = this.theDbal.SelectAll("Clients");

            foreach (DataRow r in myTable.Rows)

            {
                listClient.Add(new Client((int)r["id"], (string)r["nom"], (string)r["prenom"], (int)r["telephone"], (string)r["mail"], (int)r["credit"], (DateTime)r["dateNaissance"], (int)r["NbPartie"]));
            }
            return listClient;
        }

        public Client SelectById(int id)
        {

            DataRow rowClient = this.theDbal.SelectById("Clients", id);
            return new Client((int)rowClient["id"], (string)rowClient["nom"], (string)rowClient["prenom"], (int)rowClient["telephone"], (string)rowClient["mail"], (int)rowClient["credit"], (DateTime)rowClient["dateNaissance"], (int)rowClient["NbPartie"]);
        }

        public Client SelectByName(string name)
        {
            string search = "nom = '" + name + "'";

            DataTable tableClient = this.theDbal.SelectByField("Clients", search);
            return new Client((int)tableClient.Rows[0]["id"], (string)tableClient.Rows[0]["nom"], (string)tableClient.Rows[0]["prenom"], (int)tableClient.Rows[0]["telephone"], (string)tableClient.Rows[0]["mail"], (int)tableClient.Rows[0]["credit"], (DateTime)tableClient.Rows[0]["dateNaissance"], (int)tableClient.Rows[0]["NbPartie"]);
        }

        public void Delete(Client unCLient)
        {
            string query = " Clients where id = " + unCLient.Id ;

            this.theDbal.Delete(query);

        }
    }
}
