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
        
        public string Operation { get; set; }
        public string Recherche { get; set; }
        

        public bool[] ModeArray
        {
            get {
                //if (_modeArray[0] == true)
                //{
                //    listReservationAF.Clear();
                //}
                //if (_modeArray[1] == true)
                //{
                //    listReservationAF = new ObservableCollection<Reservation>(LesReservSansTransac(vmDaoReservation, vmDaoTransaction));
                //}
                return _modeArray;
            }
                 // Creation du tableau avec les valeurs des rd
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
            get
            {
                if (leCli != null)
                {
                    return leCli.Nom;
                }
                else
                {
                    return null;
                }
            }
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
            get
            {
                if (leCli != null)
                {
                    return leCli.Credit;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (leCli.Credit != value)
                {
                    leCli.Credit = value;
                    OnPropertyChanged("Credit");
                }
            }
        }

        public int Montant 
        {
            get;    set;
        }



        public int NbPartie
        {
            get
            {
                if (leCli != null)
                {
                    return leCli.Nbpartie;
                }
                else
                {
                    return 0;
                }
            }
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
                    if (reservChoix != null)
                    {
                        if (Montant != reservChoix.Prix)
                        {
                            Montant = reservChoix.Prix;
                            OnPropertyChanged("Montant");
                        }
                    }
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

        public List<Reservation> LesReservSansTransacClient(DaoReservation theDaoReservation, DaoTransaction theDaoTransaction, Client unCli)
        {
            List<Reservation> pasFini = new List<Reservation>();
            List<Reservation> lesReserv = new List<Reservation>(theDaoReservation.SearchReservClient(Client.Id));
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
            listClient = new ObservableCollection<Client>(theDaoClient.SelectAllSauf());
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
            Transaction maTransac = new Transaction(0, Operation, Montant, ReservChoix, Client, mondt);
            if (ModeArray[0] == true)
            {
                maTransac.Id = id;
                maTransac.Operation = "C";
                maTransac.DateTransac = DateTime.Now;
                maTransac.Reservation = vmDaoReservation.SelectbyId(1);
                maTransac.IdClient.Credit = maTransac.IdClient.Credit + maTransac.Montant;
                vmDaoClient.Update(maTransac.IdClient);
                OnPropertyChanged("Credit");
                MessageBox.Show("Votre client est crédité", "Confirmation crédit client", MessageBoxButton.OK);
            }
            if (ModeArray[1] == true)
            {
                testi = vmDaoTransaction.TestCreditMontant(maTransac.IdClient.Credit, reservChoix.Prix);
                if (testi == true)
                {
                    maTransac.Id = id;
                    maTransac.IdClient = reservChoix.IdClient;
                    maTransac.Montant = reservChoix.Prix;
                    maTransac.Operation = "D";
                    maTransac.Reservation = ReservChoix;
                    maTransac.DateTransac = DateTime.Now;
                    maTransac.IdClient.Credit = maTransac.IdClient.Credit - reservChoix.Prix;
                    vmDaoClient.Update(maTransac.IdClient);
                    OnPropertyChanged("Credit");
                    MessageBox.Show("Votre client est débité", "Confirmation débit client", MessageBoxButton.OK);
                }
            }
            vmDaoTransaction.InsertTransaction(maTransac);
            List<Reservation> maliste = new List<Reservation>(LesReservSansTransac(vmDaoReservation, vmDaoTransaction));
            listReservationAF.Clear();
            foreach(Reservation r in maliste)
            {
                listReservationAF.Add(r);
            }
           
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
            ObservableCollection<Client> lalistClient = new ObservableCollection<Client>(vmDaoClient.SelectAllSauf());
            listClient.Clear();
            foreach (Client c in lalistClient)
            {
                listClient.Add(c);
            }
        }

        public void RefreshListReservation()
        {
            ObservableCollection<Reservation> lalistReserv = new ObservableCollection<Reservation>(LesReservSansTransac(vmDaoReservation, vmDaoTransaction));
            listReservationAF.Clear();
            foreach (Reservation r in listReservationAF)
            {
                listReservationAF.Add(r);
            }
        }
        private void Rechercher()
        {
            if (this.Recherche != "")
            {
                if (this.Recherche.Contains(" "))
                {
                    string vari = Recherche.ToString();
                    string[] mot = vari.Split(' ');

                    List<Client> listClienIndep1 = new List<Client>(vmDaoClient.SearchbyName("Clients", "Nom Like '" + mot[0] + "' or Prenom like '" + mot[1] + "' or Nom Like '" + mot[1] + "' or Prenom Like '" + mot[0] +  "'"));
                    listClient.Clear();
                    foreach (Client c in listClienIndep1)
                    {
                        listClient.Add(c);
                    }
                }
                else
                {

                    List<Client> listClienIndep = new List<Client>(vmDaoClient.SearchbyName("Clients", "Nom Like '" + this.Recherche + "' or Prenom like '" + this.Recherche + "'"));
                    listClient.Clear();
                    foreach (Client c in listClienIndep)
                    {
                        listClient.Add(c);
                    }
                }
            }
            else RefreshListCli();
        }
    }

}
