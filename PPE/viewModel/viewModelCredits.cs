using System;
using System.Windows;
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
    class viewModelCredits : viewModelBase
    {
        private bool[] _modeArray = new bool[] { true, false };
        private DaoClient vmDaoClient;
        private DaoTransaction vmDaoTransaction;
        private ICommand insertCommand;
        private ObservableCollection<Client> listClient;
        private ObservableCollection<Transaction> listTransaction;
        private Client leCli = new Client();
        private Transaction laTransaction = new Transaction();
        private ObservableCollection<Reservation> listReservationAF;
        private ICommand searchCommand;
        public ObservableCollection<Client> ListClient { get => listClient; set => listClient = value; }
        public ObservableCollection<Transaction> ListTransaction { get => listTransaction; set => listTransaction = value; }
        public ObservableCollection<Reservation> ListReservationAF { get => listReservationAF; set => listReservationAF = value; }
        public int Montant { get; set; }
        public string Operation { get; set; }
        public string Recherche { get; set; }
        

        public bool[] ModeArray
        {
            get { 
                return _modeArray; }
        }
        public int SelectedMode
        {
            get { return Array.IndexOf(_modeArray, true); }
        }
        public Client Client
        {
            get => leCli;
            set
            {
                if (leCli != value) 
                {
                    leCli = value;
                    OnPropertyChanged("Client");
                    OnPropertyChanged("Nom");
                    OnPropertyChanged("Credit");
                    OnPropertyChanged("NbPartie");
                }

            }
        }

        public string Nom
        {
            get => leCli.Nom;
            set
            {
                if (leCli.Nom != value)
                {
                    leCli.Nom = value;
                    OnPropertyChanged("Nom");
                }
            }
        }

        
        public int Credit
        {
            get => leCli.Credit;
            set
            {
                if (leCli.Credit != value)
                {
                    leCli.Credit = value;
                    OnPropertyChanged("Credit");
                }
            }
        }

        public int NbPartie
        {
            get => leCli.Nbpartie;
            set
            {
                if (leCli.Nbpartie != value) 
                {
                    leCli.Nbpartie = value;
                    OnPropertyChanged("NbPartie");
                }
            }
        }

          
       

        public viewModelCredits(DaoClient theDaoClient, DaoTransaction theDaoTransac)
        {
            vmDaoClient = theDaoClient;
            vmDaoTransaction = theDaoTransac;
            listClient = new ObservableCollection<Client>(theDaoClient.SelectAll());
            listReservationAF = new ObservableCollection<Reservation>(theDaoTransac.LesReservSansTransac());
        }

        public ICommand Valider
        {
            get
            {
                if (this.insertCommand == null)
                {
                    this.insertCommand = new RelayCommand(() => InsertTransac(), () => true);
               }
               return this.insertCommand;
            }
        }

        private void InsertTransac()
        {
            if (this.ModeArray[0] == true)
            {
                int id = vmDaoTransaction.ReturnnextId();
                Transaction maTransac = new Transaction(id, this.Operation, this.Montant, this.leCli);
                vmDaoTransaction.Insert(maTransac);
            }
            else
            {
                // foreach (Client c in listClient)
                // {
                //    idf = idf + 1;
                // }
                // listClient.Add(Client);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                if (this.searchCommand == null)
                {
                    this.searchCommand = new RelayCommand(() => Rechercher(), () => true);
                }
                return this.searchCommand;

            }
        }

        public void RefreshListCli()
        {
            ObservableCollection<Client> lalistClient = new ObservableCollection<Client>(vmDaoClient.SelectAll());
            listClient.Clear();
            foreach (Client c in lalistClient)
            {
                listClient.Add(c);
            }
        }

        private void Rechercher()
        {
            if (this.Recherche != "")
            {
                List<Client> listClienIndep = new List<Client>(vmDaoClient.SearchbyName("Clients", "Nom Like '" + this.Recherche + "' or Prenom like '" + this.Recherche + "'"));
                listClient.Clear();
                foreach (Client c in listClienIndep)
                {
                    listClient.Add(c);
                }
            }
            else RefreshListCli();
        }
    }
}
