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
        }

    
        
    }
}
