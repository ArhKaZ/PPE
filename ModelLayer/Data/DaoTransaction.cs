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
            DataRow myRow = mydbal.SelectLastId("Reservation");
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

        
        public void InsertTransaction(Transaction uneTransac)
        {
                int id = ReturnnextId();
                string query = "Transactions (id, operation, montant, reservation, idclient, dateTransac) VALUES ("
                    + id + ",'"
                    + uneTransac.Operation + "',"
                    + uneTransac.Montant + ","
                    + uneTransac.Reservation.Id + ","
                    + uneTransac.IdClient.Id + ",'"
                    + uneTransac.DateTransac + "')";
          
            
            this.mydbal.Insert(query);
        }

        public List<Transaction> SelectAll()
        {
            List<Transaction> listTransaction = new List<Transaction>();
            DataTable rowTransaction = this.mydbal.SelectAll("transactions");

            foreach (DataRow r in rowTransaction.Rows)
            {
                Client unCli = this.theDaoClient.SelectById((int)r["idClient"]);
                Reservation uneReserv = this.theDaoReservation.SelectbyId((int)r["Reservation"]);
                listTransaction.Add(new Transaction((int)r["id"], (string)r["operation"], (int)r["montant"], uneReserv, unCli));
            }
            return listTransaction;
        }

        public Transaction SelectById(int id)
        {
            DataRow rowTransaction = this.mydbal.SelectById("transactions", id);
            Reservation uneResevation = this.theDaoReservation.SelectbyId((int)rowTransaction["reservation"]);
            Client unCli = this.theDaoClient.SelectById((int)rowTransaction["idClient"]);
            return new Transaction((int)rowTransaction["id"],
                (string)rowTransaction["operation"],
                (int)rowTransaction["montant"],
                uneResevation,
                unCli);
        }
    }
}
