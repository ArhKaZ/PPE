using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Business
{
    public class Transaction
    {
        private int id;
        private string operation;
        private int montant;
        private Reservation reservation;
        private Client idClient;
        private DateTime dateTransac;
        public Transaction(int id, string operation, int montant, Reservation reservation, Client idClient)
        {
            Id = id;
            Operation = operation;
            Montant = montant;
            Reservation = reservation;
            IdClient = idClient;
            DateTransac = DateTime.Now;
        }

        public Transaction(int id, string operation, int montant, int reservation, Client idClient)
        {
            Id = id;
            Operation = operation;
            Montant = montant;
            List<Transaction> reserv = new List<Transaction>();
            foreach (Reservation r in reserv)
            {
                if (r.)
            }
            Reservation = reservation;
            IdClient = idClient;
            DateTransac = DateTime.Now;
        }

        public Transaction(int id, string operation, int montant, Client idClient)
        {
            Id = id;
            Operation = operation;
            Montant = montant;
            Reservation = reservation;
            IdClient = idClient;
            DateTransac = DateTime.Now;
        }

        public Transaction() { }

        public int Id { get => id ; set => id = value; }
        public string Operation { get => operation; set => operation = value; }
        public int Montant { get => montant; set => montant= value; }
        public Reservation Reservation { get => reservation; set => reservation = value; }
        public Client IdClient { get => idClient; set => idClient = value; }
        public DateTime DateTransac { get => dateTransac; set => dateTransac = value; }
    }
}
