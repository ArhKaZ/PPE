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
         
        
        public void Insert(Transaction uneTransac)
        {
            string query = "Transaction (id, operation, montant, reservation, idclient) VALUES ("
                + uneTransac.Id + ",'"
                + uneTransac.Operation + "',"
                + uneTransac.Montant + ","
                + uneTransac.Reservation.Id + ","
                + uneTransac.IdClient.Id + ")";
            this.mydbal.Insert(query);
        }

        public void Update(Transaction uneTransac)
        {
            string query = "Transaction set id = " + uneTransac.Id
                + ", operation = '" + uneTransac.Operation
                + ", montant = " + uneTransac.Montant
                + ", reservation = " + uneTransac.Reservation.Id
                + ", idClient = " + uneTransac.IdClient.Id + ")";
            this.mydbal.Update(query);
        }

        public void Delete(Transaction uneTransac)
        {
            string query = "Transaction Where id = " + uneTransac.Id + ")";
            this.mydbal.Delete(query);
        }

        public List<Transaction> SelectAll()
        {
            List<Transaction> listTransaction = new List<Transaction>();
            DataTable rowTransaction = this.mydbal.SelectAll("transactions");

            foreach (DataRow r in rowTransaction.Rows)
            {
                Client unCli = this.theDaoClient.SelectById((int)r["idClient"]);
                Reservation uneReserv = this.theDaoReservation.SelectbyId((int)r["idReservation"]);
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
