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
using GalaSoft.MvvmLight.Command;

namespace PPE.viewModel
{
    class viewModelConnexion : viewModelBase
    {
        private DaoUtilisateur vmDaoUser;
        private ICommand validCo;
        private ObservableCollection<Utilisateur> listUser;
        private Utilisateur unUser = new Utilisateur();
        
        public RelayCommand<Window> CloseWndCmd { get; private set; }

        public ObservableCollection<Utilisateur> ListUser { get => listUser; set => listUser = value; }
       
        public viewModelConnexion(DaoUtilisateur theDaoUtilisateur)
        {
            vmDaoUser = theDaoUtilisateur;
            listUser = new ObservableCollection<Utilisateur>(theDaoUtilisateur.SelectAll());
            this.CloseWndCmd = new RelayCommand<Window>(this.CloseWnd);
        }
        
        private void CloseWnd(Window WndConnexion)
        {
            if (WndConnexion != null)
            {
                WndConnexion.Close();
            }
        }
        public Utilisateur LeUser
        {
            get => unUser;
            set
            {
                unUser.Identifiant = NomUser;
                unUser.Mdp = Mdp;
            }
        }
        public string NomUser
        {
            get
            {
                return NomUser;
            }
        }

        public string Mdp
        {
            get
            {
                return Mdp;
            }
        }

        public bool ValiderConnexion(Window wndConnexion)
        {
            bool res = false;
            foreach (Utilisateur u in listUser)
            {
                if (u.Identifiant == LeUser.Identifiant)
                {
                    if (u.Mdp == LeUser.Mdp)
                    {
                        this.CloseWnd(wndConnexion);
                        return res = true;
                    }
                    else
                    {
                        MessageBox.Show("Le mot de passe saisit n'est pas bon");
                        return res = false;   
                    }
                }
                else
                {
                    MessageBox.Show("Les identifiants saisit ne sont pas bon");
                    return res = false;
                }
            }
            return res;
        }
    }
}
