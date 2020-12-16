using System;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ModelLayer.Business;
using ModelLayer.Data;

namespace PPE.viewModel
{
    class viewModelTransactions : viewModelBase
    {
        private bool[] _modeArray = new bool[] { true, false };
        private DaoTransaction vmDaoTransaction;
        private ICommand updateCommand;
        private ObservableCollection<Transaction> listTransaction;
        private ObservableCollection<Client> listClient;
        private Transaction maTransac = new Transaction();
       
        public ObservableCollection<Transaction> ListTransaction { get => listTransaction; set => listTransaction = value; }
        public ObservableCollection<Client> ListClient { get => listClient; set => listClient = value; }

        public viewModelTransactions(DaoTransaction theDaoTransac)
        {
            vmDaoTransaction = theDaoTransac;
            listTransaction = new ObservableCollection<Transaction>(theDaoTransac.SelectAll());
        }
        public Transaction Transaction
        {
            get => maTransac;
            set
            {
                if (maTransac != value)
                {
                    maTransac = value;
                    OnPropertyChanged("Ville");
                    OnPropertyChanged("Prix");
                    OnPropertyChanged("Numero");
                    OnPropertyChanged("Nom");
                }
            }
        }

        public string Nom
        {
            get => maTransac.IdClient.Nom;
            set
            {
                if (maTransac.IdClient.Nom != value)
                {
                    maTransac.IdClient.Nom = value;
                    OnPropertyChanged("Nom");
                }
            }
        }
        public MouseBinding SetNull
        {
            set
            {
                maTransac = null;
            }
        }
        public string Ville
        {
            get => maTransac.Reservation.IdSalle.IdLieu.Nom;
            set
            {
                if (maTransac.Reservation.IdSalle.IdLieu.Nom != value)
                {
                    maTransac.Reservation.IdSalle.IdLieu.Nom = value;
                    OnPropertyChanged("Ville");
                }

            }
        }

        public int Prix
        {
            get => maTransac.Montant;
            set
            {
                if (maTransac.Montant != value)
                {
                    maTransac.Montant = value;
                    OnPropertyChanged("Prix");
                }
            }
        }

        public int Numero
        {
            get => maTransac.Id;
            set
            {
                if (maTransac.Id != value)
                {
                    maTransac.Id = value;
                    OnPropertyChanged("Numero");
                }
            }
        }

       
        public bool[] ModeArray
        {
            get { return _modeArray; }
        }
        public int SelectedMode
        {
            get { return Array.IndexOf(_modeArray, true); }
        }

    }
}
