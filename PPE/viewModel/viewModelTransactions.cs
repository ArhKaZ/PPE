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
        private Transaction maTransac;
       
        public ObservableCollection<Transaction> ListTransaction { get => listTransaction; set => listTransaction = value; }
        public ObservableCollection<Client> ListClient { get => listClient; set => listClient = value; }

        public viewModelTransactions(DaoTransaction theDaoTransac)
        {
            vmDaoTransaction = theDaoTransac;
            listTransaction = new ObservableCollection<Transaction>(theDaoTransac.SelectAll());
        }
        public Transaction Transaction
        {
            get
            {
                if (maTransac != null)
                {
                    return maTransac;
                }
                else
                {
                    return null;
                }
            }
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
            get
            {
                if (maTransac != null)
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
                if (maTransac != null)
                {
                    return maTransac.Reservation.IdSalle.IdLieu.Nom; ;
                }
                else
                {
                    return null;
                }
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
                    return maTransac.Montant ;
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
                    return 0;
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

        public ICommand UpdateCommand
        {
            get
            {
                if (this.updateCommand == null)
                {
                    this.updateCommand = new RelayCommand(() => UpdateTransac(), () => true);
                }
                return this.updateCommand;
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

        public void UpdateTransac()
        {
            Transaction Transactionsauv = new Transaction();
            vmDaoTransaction.Update(Transaction);
            int index = listTransaction.IndexOf(Transaction);
            listTransaction.Insert(index, Transaction);
            listTransaction.RemoveAt(index + 1);
            Transaction = Transactionsauv;
        }
    }
}
