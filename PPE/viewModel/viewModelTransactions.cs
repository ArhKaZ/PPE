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
        private DaoClient vmDaoClient;
        private DaoReservation vmDaoReservation;
        private ICommand valider;
        private ICommand refreshCommand;
        private ICommand searchCommand;
        private ObservableCollection<Transaction> listTransaction;
        private ObservableCollection<Client> listClient;

        private Transaction maTransac = new Transaction();
        private Client monCli = new Client();
        public ObservableCollection<Transaction> ListTransaction { get => listTransaction; set => listTransaction = value; }
        public ObservableCollection<Client> ListClient { get => listClient; set => listClient = value; }
        public string RentreValider {get; set;}
        public string Recherche { get; set; }
        public viewModelTransactions(DaoTransaction theDaoTransac, DaoClient theDaoClient, DaoReservation theDaoReservation)
        {
            vmDaoTransaction = theDaoTransac;
            vmDaoClient = theDaoClient;
            vmDaoReservation = theDaoReservation;
            listTransaction = new ObservableCollection<Transaction>(theDaoTransac.SelectAllSauf());
            monCli = vmDaoClient.SelectById(1);
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
                    OnPropertyChanged("DateTransac");
                }
            }
        }

        public string Nom
        {
            get 
            {
                if (maTransac.IdClient != null)
                {
                    return maTransac.IdClient.Nom;
                }
                else
                {
                    return null;
                }
            }
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
            get 
            {
                if (maTransac.Reservation == null)
                {
                    return null;
                }
                else
                {
                    return maTransac.Reservation.IdSalle.IdLieu.Nom;
                }
                //if (maTransac.Reservation != null)
                //{
                //    return maTransac.Reservation.IdSalle.IdLieu.Nom;
                //}
                //else
                //{
                //    return null;
                //}
            }

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
            get
            {
                if (maTransac != null)
                {
                    return maTransac.Montant;
                }
                else
                {
                    return 0;
                }
            }
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
            get 
            {
                if (maTransac != null)
                {
                    return maTransac.Id;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (maTransac.Id != value)
                {
                    maTransac.Id = value;
                    OnPropertyChanged("Numero");
                }
            }
        }

        public DateTime DateTransac
        {
            get
            {
                if (maTransac != null)
                {
                    return maTransac.DateTransac;
                }
                else
                {
                    return new DateTime();
                }
            }
            set
            {
                if (maTransac.DateTransac != value)
                {
                    maTransac.DateTransac = value;
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

        public void RefreshList()
        {
            ObservableCollection<Transaction> lalistTransac = new ObservableCollection<Transaction>(vmDaoTransaction.SelectAllSauf());
             listTransaction.Clear();
            foreach (Transaction c in lalistTransac)
            {
                listTransaction.Add(c);
            }
        }

        public void Trier()
        {
            List<Transaction> list = new List<Transaction>(vmDaoTransaction.SelectAll());
            listTransaction.Clear();
            if (ModeArray[0] == true)
            {
                if (RentreValider == "")
                {
                    RefreshList();
                }
                else
                {
                    foreach (Transaction t in list)
                    {
                        if (t.Reservation.IdSalle.IdLieu.Nom.ToLower() == RentreValider.ToLower())
                        {
                            listTransaction.Add(t);
                        }
                    }
                }
            }
            if (ModeArray[1] == true)
            {
                if (RentreValider == "")
                {
                    RefreshList();
                }
                else
                {
                    foreach (Transaction t in list)
                    {
                        if (t.IdClient.Nom == RentreValider || t.IdClient.Prenom == RentreValider)
                        {
                            listTransaction.Add(t);
                        }
                    }
                }
            }
        }
        
       
        public ICommand Valider
        {
            get
            {
                if (this.valider == null)
                {
                    this.valider = new RelayCommand(() => Trier(), () => true);
                }
                return this.valider;
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                if (this.refreshCommand == null)
                {
                    this.refreshCommand = new RelayCommand(() => RefreshList(), () => true);
                }
                return this.refreshCommand;
            }
        }
    }
}
