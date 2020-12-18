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
    class viewModelClient : viewModelBase
    {
        private DaoClient vmDaoClient;
        private ICommand insertCommand;
        private ICommand updateCommand;
        private ICommand deleteCommand;
        private ICommand searchCommand;
        private ObservableCollection<Client> listClient;
        private Client leCli = new Client();

        public string Recherche { get; set; }
        public ObservableCollection<Client> ListClient { get => listClient; set => listClient = value; }
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
                    OnPropertyChanged("Prenom");
                    OnPropertyChanged("Telephone");
                    OnPropertyChanged("Mail");
                    OnPropertyChanged("Credit");
                    OnPropertyChanged("DateNaissance");
                    OnPropertyChanged("Photo");
                    OnPropertyChanged("NbPartie");
                }
            }
        }
        public viewModelClient(DaoClient theDaoClient)
        {
            vmDaoClient = theDaoClient;
            listClient = new ObservableCollection<Client>(vmDaoClient.SelectAllSauf());
        }

        public string Nom
        {

            get
            { //RAJOUTE UN BTN POUR REFRESH
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

        public MouseBinding SetNull
        {
            set
            {
                leCli = null;
            }
        }
        public string Prenom
        {
            get
            {
                if (leCli != null)
                {
                    return leCli.Prenom;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (leCli.Prenom != value)
                {
                    leCli.Prenom = value;
                    OnPropertyChanged("Prenom");
                }
            }
        }

        public int Telephone
        {
            get
            {
                if (leCli != null)
                {
                    return leCli.Telephone;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (leCli.Telephone != value)
                {
                    leCli.Telephone = value;
                    OnPropertyChanged("Telephone");
                }
            }
        }

        public string Mail
        {
            get
            {
                if (leCli != null)
                {
                    return leCli.Mail;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (leCli.Mail != value)
                {
                    leCli.Mail = value;
                    OnPropertyChanged("Mail");
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

        public DateTime DateNaissance
        {
            get
            {
                if (leCli != null)
                {
                    return leCli.DateNaissance;
                }
                else
                {
                    return new DateTime();
                }
            }
            set
            {
                if (leCli.DateNaissance != value)
                {

                    leCli.DateNaissance = value;

                }
            }
        }

        public string Photo
        {
            get
            {
                if (leCli != null)
                {
                    return leCli.Photo;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (leCli.Photo != value)
                {
                    leCli.Photo = value;
                    OnPropertyChanged("Photo");
                }
            }
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

        public void RefreshListCli()
        {
            ObservableCollection<Client> lalistClient = new ObservableCollection<Client>(vmDaoClient.SelectAllSauf());
            listClient.Clear();
            foreach (Client c in lalistClient)
            {
                listClient.Add(c);
            }
        }
        public ICommand UpdateCommand
        {
            get
            {
                if (this.updateCommand == null)
                {
                    this.updateCommand = new RelayCommand(() => UpdateClient(), () => true);
                }

                return this.updateCommand;

            }

        }
        public ICommand InsertCommand
        {
            get
            {
                if (this.insertCommand == null)
                {
                    this.insertCommand = new RelayCommand(() => InsertClient(), () => true);
                }
                return this.insertCommand;

            }

        }
        public ICommand DeleteCommand
        {
            get
            {
                if (this.deleteCommand == null)
                {
                    this.deleteCommand = new RelayCommand(() => DeleteClient(), () => true);
                }
                return this.deleteCommand;

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

        private void UpdateClient()
        {

            vmDaoClient.Update(leCli);
            RefreshListCli();
            MessageBox.Show("Votre client est modifier", "Confirmation modification client", MessageBoxButton.OK);
        }

        private void InsertClient() //Ajouter lien avec listclient du 2nd onglet
        {
            leCli.Credit = 0;
            vmDaoClient.Insert(leCli);
            RefreshListCli();
            MessageBox.Show("Votre client est ajouté", "Confirmation nouveau client", MessageBoxButton.OK);
        }

        private void DeleteClient()
        {
            int index = listClient.IndexOf(Client);
            vmDaoClient.Delete(leCli);
            //listClient.Remove(Client);
            RefreshListCli();
            MessageBox.Show("Votre client est supprimmer", "Confirmation suppression client", MessageBoxButton.OK);
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

