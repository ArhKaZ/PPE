using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ModelLayer.Data;
using PPE.viewModel;
using ModelLayer.Business;
namespace PPE
{
    /// <summary>
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Window
    {
        private DaoUtilisateur vmDaoUser;
        private DaoClient vmDaoClient;
        private DaoTransaction vmDaoTransac;
        private List<Utilisateur> listUser;
        
        private DaoVille vmDaoVille;
        public Connexion(DaoClient theDaoClient,DaoTransaction theDaoTransaction, DaoUtilisateur theDaoUtilisateur,DaoVille theDaoVille)
        {
            vmDaoVille = theDaoVille;
            vmDaoUser = theDaoUtilisateur;
            listUser = new List<Utilisateur>(vmDaoUser.SelectAll());
            vmDaoClient = theDaoClient;
            vmDaoTransac = theDaoTransaction;
            InitializeComponent();
            
        }

        private void Btn_co_Click(object sender, RoutedEventArgs e)
        {
            Utilisateur unUser = new Utilisateur(Txt_box_user.Text, Txt_box_mdp.Text);
           
            foreach (Utilisateur u in listUser)
            {
               
                if (u.Identifiant == unUser.Identifiant)
                {
                    if (u.Mdp == unUser.Mdp)
                    {
                        this.Close();

                    }
                    
                }
                
                
            }
            if (this.IsActive == true)
            {
                MessageBox.Show("Vos identifians ne sont pas bon");
            }
        }

        private void WndConnexion_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow wnd = new MainWindow(vmDaoClient, vmDaoTransac);
            wnd.Show();
        }
    }
    }

