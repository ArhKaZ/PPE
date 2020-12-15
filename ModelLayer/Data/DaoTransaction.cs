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

        public List<Reservation> LesReservSansTransac()
        {
            List<Reservation> pasFini = new List<Reservation>();
            List<Reservation> lesReserv = new List<Reservation>(theDaoReservation.SelectAll());
            List<Transaction> lesTransac = new List<Transaction>(theDaoTransaction.SelectAll());
            foreach (Reservation r in lesReserv)
            {
                foreach (Transaction t in lesTransac) 
                {
                    if (t.Reservation.Id == r.Id)
                    {
                        pasFini.Add(r);
                    }
                }
            }
            return pasFini;
        }

        //public bool TestCreditMontant(Client uncli, int Montant)
        //{
        //    if ()
        //        if (Montant > uncli.Credit)
        //        {

        //        }
        //}
        public void Insert(Transaction uneTransac)
        {
            int id = ReturnnextId();
            string query = "Transaction (id, operation, montant, reservation, idclient) VALUES ("
                + id + ",'"
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
