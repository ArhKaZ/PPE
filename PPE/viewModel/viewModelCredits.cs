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
    class viewModelCredits : viewModelBase
    {
        private bool[] _modeArray = new bool[] { true, false };
        private DaoClient vmDaoClient;
        private DaoTransaction vmDaoTransaction;
        private DaoReservation vmDaoReservation;
        private DaoSalle vmDaoSalle;
        private DaoUtilisateur vmDaoUtilisateur;
        private ICommand insertCommand;
        private ICommand searchCommand;
        private ICommand refreshCommand;
        private ObservableCollection<Client> listClient;
        private ObservableCollection<Transaction> listTransaction;
        private Client leCli = new Client();
        private Reservation reservChoix = new Reservation();
        private Transaction laTransaction = new Transaction();
        private ObservableCollection<Reservation> listReservationAF;
        //private ObservableCollection<Reservation> listReservation;
        //Public
        public ObservableCollection<Client> ListClient { get => listClient; set => listClient = value; }
        public ObservableCollection<Transaction> ListTransaction { get => listTransaction; set => listTransaction = value; }
        public ObservableCollection<Reservation> ListReservationAF { get => listReservationAF; set => listReservationAF = value; }
        //public ObservableCollection<Reservation> ListReservation { get => listReservation; set => listReservation = value; }
        public int Montant { get; set; }
        public string Operation { get; set; }
        public string Recherche { get; set; }
        

        public bool[] ModeArray
        {
            get { return _modeArray; } // Creation du tableau avec les valeurs des rd
        }                               
        public int SelectedMode
        {
            get { return Array.IndexOf(_modeArray, true); } // Sors quel rd est coché
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

        public Reservation ReservChoix
        {
            get => reservChoix;
            set
            {
                if (reservChoix != value)
                {
                    reservChoix = value;
                    OnPropertyChanged("ReservChoix");
                }
            }
        }


        public List<Reservation> LesReservSansTransac(DaoReservation theDaoReservation, DaoTransaction theDaoTransaction)
        {
            List<Reservation> pasFini = new List<Reservation>();
            List<Reservation> lesReserv = new List<Reservation>(theDaoReservation.SelectAll());
            List<int> lesTransac = new List<int>(theDaoTransaction.ReturnAllIdReserv());
            foreach (Reservation r in lesReserv)
            {
                if (lesTransac.Contains(r.Id) == false)
                {
                    pasFini.Add(r);
                }
            }
            return pasFini;
        }

            public viewModelCredits(DaoClient theDaoClient, DaoTransaction theDaoTransac, DaoSalle theDaoSalle, DaoReservation theDaoReservation, DaoUtilisateur theDaoUtilisateur)
        {
            vmDaoClient = theDaoClient;
            vmDaoReservation = theDaoReservation;
            vmDaoTransaction = theDaoTransac;
            vmDaoSalle = theDaoSalle;
            vmDaoUtilisateur = theDaoUtilisateur;
            listClient = new ObservableCollection<Client>(theDaoClient.SelectAll());
            listTransaction = new ObservableCollection<Transaction>(theDaoTransac.SelectAll());
            listReservationAF = new ObservableCollection<Reservation>(LesReservSansTransac(vmDaoReservation, vmDaoTransaction));
            RefreshListCli();
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
            
            bool testi = true;
            DateTime mondt = new DateTime();
            mondt = DateTime.Now;
            int id = vmDaoTransaction.ReturnnextId();
            Transaction maTransac = new Transaction(0, Operation, Montant, ReservChoix, Client);
            if (ModeArray[0] == true)
            {
                maTransac.Id = id;
                maTransac.Operation = "C";
                maTransac.DateTransac = mondt;
                maTransac.Reservation = vmDaoReservation.SelectbyId(1);
                
            }
            if (ModeArray[1] == true)
            {
                testi = vmDaoTransaction.TestCreditMontant(maTransac.IdClient.Credit, maTransac.Montant);
                if (testi == true)
                {
                    maTransac.Id = id;
                    maTransac.IdClient = leCli;
                    maTransac.Montant = Montant;
                    maTransac.Operation = "D";
                    maTransac.Reservation = ReservChoix;
                    maTransac.DateTransac = DateTime.Now;
                    
                }
            }
            vmDaoTransaction.InsertTransaction(maTransac);
        }
        public ICommand RefreshCommand
        {
            get
            {
                if (this.refreshCommand == null)
                {
                    this.refreshCommand = new RelayCommand(() => RefreshListCli(), () => true);
                }
                return this.refreshCommand;
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
