using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Business;
using ModelLayer.Data;
using System.Windows.Input;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DevExpress.Mvvm;
using GalaSoft.MvvmLight.Command;
using PPE.inter_face;

namespace PPE.viewModel
{
    class viewModelConnexion : viewModelBase
    {
       
        private DaoUtilisateur vmDaoUtilisateur;
        private ObservableCollection<Utilisateur> listUtilisateur;
        private Utilisateur leUser = new Utilisateur();
        private ICommand connexionCommand;
        public viewModelConnexion(DaoUtilisateur theDaoUtilisateur)
        {
            vmDaoUtilisateur = theDaoUtilisateur;
        }


        public Utilisateur Utilisateur
        {
           // get => leUser;
            set
            {
                if (leUser != value)
                {
                    leUser = value;
                    OnPropertyChanged("Identifiant");
                    OnPropertyChanged("Mdp");
                }
            }
        }
        public string Identifiant
        { 
            get
            {
                if (leUser != null)
                {
                    return leUser.Identifiant;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (leUser.Identifiant != value)
                {
                    leUser.Identifiant = value;
                    OnPropertyChanged("Identifiant");
                }
            }
        }

        public string Mdp
        {
            get
            {
                if (leUser != null)
                {
                    return leUser.Mdp;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (leUser.Mdp != value)
                {
                    leUser.Mdp = value;
                    OnPropertyChanged("Mdp");
                }
            }
        }

        //public void VerifConnexion()
        //{
        //    ObservableCollection<Utilisateur> lalistUser = new ObservableCollection<Utilisateur>(vmDaoUtilisateur.SelectAll());
        //    foreach (Utilisateur u in lalistUser)
        //    {
        //        if (u.Identifiant == leUser.Identifiant && u.Mdp == leUser.Mdp)
        //        {
                   
        //        }
        //    }
        //}

        //public ICommand ConnexionCommand
        //{
        //    get
        //    {
        //        if (this.connexionCommand == null)
        //        {
        //            this.connexionCommand = new RelayCommand(() => VerifConnexion(), () => true);
        //        }
        //        return this.connexionCommand;
        //    }
        //}

       public RelayCommand<ICloseable> CloseWindowCommand { get; private set; }

        public viewModelConnexion()
        {
            this.CloseWindowCommand = new RelayCommand<ICloseable>(this.CloseWindow);
        }

        private void CloseWindow(ICloseable Window)
        {
            if (Window != null)
            {
                Window.Close();
            }
        }

    }
}
