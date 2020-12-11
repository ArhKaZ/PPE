using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
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
        
        //Public
        public ObservableCollection<Client> ListClient { get => listClient; set => listClient = value; }
        public ObservableCollection<Transaction> ListTransaction { get => listTransaction; set => listTransaction = value; }

        public Transaction Transaction
        {
            get => laTransaction;
            set
            {
                if (laTransaction != value)
                {
                    DaoTransaction theDaoTransaction = vmDaoTransaction;
                    DaoClient theDaoClient = vmDaoClient;
                    laTransaction = value;
                    listTransaction = new ObservableCollection<Transaction>(theDaoTransaction.SelectAll());
                    listClient = new ObservableCollection<Client>(theDaoClient.SelectAll());
                    foreach (Transaction t in listTransaction)
                    {
                        foreach (Client c in listClient)
                        {
                            if (t.IdClient.Id == c.Id)
                            {
                                t.IdClient = c;
                            }
                        }
                    }
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
                    OnPropertyChanged("Montant");
                    OnPropertyChanged("SCredit");
                    OnPropertyChanged("Valider");
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

        public int Montant
        {
            get => laTransaction.Montant;
            set
            {
                if (laTransaction.Montant != value) 
                {
                    laTransaction.Montant = value;
                    OnPropertyChanged("Montant");
                
                }
            }
        }
       
     
            
        public string Operation
        {
            get => laTransaction.Operation;
            set
            {
                if (laTransaction.Operation != "D")
                {
                    laTransaction.Operation = value;
                    
                }
            }
        }

        public viewModelCredits(DaoClient theDaoClient, DaoTransaction theDaoTransaction)
        {
            vmDaoClient = theDaoClient;
            vmDaoTransaction = theDaoTransaction;
            listClient = new ObservableCollection<Client>(theDaoClient.SelectAll());

            listTransaction = new ObservableCollection<Transaction>(theDaoTransaction.SelectAll());

            foreach (Transaction t in listTransaction)
            {
                foreach (Client c in listClient)
                {
                    if (t.IdClient.Id == c.Id)
                    {
                        t.IdClient = c;
                    }
                }
            }
        }

        public ICommand Valider
        {
            get
            {
                if (this.insertCommand == null)
                {
                    this.insertCommand = new RelayCommand(() => InsertCommand(), () => true);
                }
                return this.insertCommand;
            }
        }

        private void InsertCommand()
        {
            int idf = 0;
            vmDaoClient.Insert(Client);
            foreach (Client c in listClient)
            {
                idf = idf + 1;
            }
            listClient.Add(Client);
        }
    }
}
