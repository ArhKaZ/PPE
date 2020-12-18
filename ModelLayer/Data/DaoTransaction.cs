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
    public class DaoTransaction
    {
        private Dbal mydbal;
        private DaoTransaction theDaoTransaction;
        private DaoClient theDaoClient;
        private DaoReservation theDaoReservation;

        public DaoTransaction(Dbal mydbal, DaoClient theDaoClient, DaoReservation theDaoReservation)
        {
            this.mydbal = mydbal;
            this.theDaoClient = theDaoClient;
            this.theDaoReservation = theDaoReservation;
        }

        public int ReturnnextId()
        {
            DataRow myRow = mydbal.SelectLastId("transactions");
            return (int)myRow["id"] + 1;
        }
        
        
        public List<int> ReturnAllIdReserv()
        {
            List<int> listInt = new List<int>();
            DataTable rowInt = this.mydbal.SelectAllIdReserv();
            foreach (DataRow r in rowInt.Rows)
            {
                listInt.Add((int) r["reservation"]);
            }
            return listInt;
        }

        public bool TestCreditMontant(int credit, int montant)
        {
            bool test = true;
                if (montant > credit)
                {
                    test = false;
                }
            return test;
        }
        //Tout marche il faut seulement finir les ajustements et afficher la date dans le dp de 
        //Transaction
        
        public void InsertTransaction(Transaction uneTransac)
        {
                int id = ReturnnextId();
                string query = "Transactions (id, operation, montant, reservation, idclient, dateTransac) VALUES ("
                    + id + ",'"
                    + uneTransac.Operation + "',"
                    + uneTransac.Montant + ","
                    + uneTransac.Reservation.Id + ","
                    + uneTransac.IdClient.Id + ",'"
                    + uneTransac.DateTransac.ToString("yyyy-MM-dd") + "')";
          
            
            this.mydbal.Insert(query);
        }
        
        public List<Transaction> SelectAll()
        {
            List<Transaction> listTransaction = new List<Transaction>();
            DataTable rowTransaction = this.mydbal.SelectAll("transactions");

            foreach (DataRow r in rowTransaction.Rows)
            {
                
                Client unCli = this.theDaoClient.SelectById((int)r["idClient"]);
                Reservation uneReserv = this.theDaoReservation.SelectbyId(1);
                Transaction temp = new Transaction((int)r["id"], (string)r["operation"], (int)r["montant"], uneReserv, unCli, (DateTime)r["dateTransac"]) ;
                if (temp.Operation == "D")
                {
                    temp.Reservation = this.theDaoReservation.SelectbyId((int)r["reservation"]);
                    listTransaction.Add(temp);
                }
                else
                {
                    listTransaction.Add(temp);
                }
                
                 
            }
            return listTransaction;
        }
        public List<Transaction> SelectAllSauf()
        {
            List<Transaction> listTransaction = new List<Transaction>();
            DataTable rowTransaction = this.mydbal.SelectAllSauf("transactions", 1);

            foreach (DataRow r in rowTransaction.Rows)
            {

                Client unCli = this.theDaoClient.SelectById((int)r["idClient"]);
                Reservation uneReserv = this.theDaoReservation.SelectbyId(1);
                Transaction temp = new Transaction((int)r["id"], (string)r["operation"], (int)r["montant"], uneReserv, unCli, (DateTime)r["dateTransac"]);
                if (temp.Operation == "D")
                {
                    temp.Reservation = this.theDaoReservation.SelectbyId((int)r["reservation"]);
                    listTransaction.Add(temp);
                }
                else
                {
                    listTransaction.Add(temp);
                }


            }
            return listTransaction;
        }

        //public DateTime SelectDtFromIdReserv(int id)
        //{
        //    DataRow rowDate = this.mydbal.SelectDate("dateTransac", "transactions", "reservation", id);
        //    return new DateTime((DateTime)rowDate["dateTransac"]);
        //}
        public Transaction SelectById(int id)
        {
            DataRow rowTransaction = this.mydbal.SelectById("transactions", id);
            Reservation uneResevation = this.theDaoReservation.SelectbyId((int)rowTransaction["reservation"]);
            Client unCli = this.theDaoClient.SelectById((int)rowTransaction["idClient"]);
            return new Transaction((int)rowTransaction["id"],
                (string)rowTransaction["operation"],
                (int)rowTransaction["montant"],
                uneResevation,
                unCli,
                (DateTime)rowTransaction["dateTransac"]);
        }

        //public Client SelectlistById(int id)
        //{
        //    string search = "id = '" + id + "'";

        //    DataTable tableClient = this.mydbal.SelectByField("Clients", search);
        //    return new Client((int)tableClient.Rows[0]["id"], (string)tableClient.Rows[0]["nom"], (string)tableClient.Rows[0]["prenom"], (int)tableClient.Rows[0]["telephone"], (string)tableClient.Rows[0]["mail"], (int)tableClient.Rows[0]["credit"], (DateTime)tableClient.Rows[0]["dateNaissance"], (int)tableClient.Rows[0]["NbPartie"]);
        //}
    }
}
