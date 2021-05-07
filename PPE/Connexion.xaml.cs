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
        private List<Utilisateur> listUser;
        private Utilisateur unUser;
        private DaoVille vmDaoVille;
        public Connexion(DaoUtilisateur theDaoUtilisateur, DaoVille theDaoVille)
        {
            vmDaoVille = theDaoVille;
            vmDaoUser = theDaoUtilisateur;
            listUser = new List<Utilisateur>(vmDaoUser.SelectAll());
            
            InitializeComponent();
        }

        private void Btn_co_Click(object sender, RoutedEventArgs e)
        {
            
            unUser.Identifiant = Txt_box_user.Text;
            unUser.Mdp = Txt_box_mdp.Text;
            
            foreach (Utilisateur u in listUser)
            {
                if (u.Identifiant == unUser.Identifiant)
                {
                    if (u.Mdp == unUser.Mdp)
                    {
                        this.Close();
                        
                    }
                    else
                    {
                        MessageBox.Show("Le mot de passe saisit n'est pas bon");
                       
                    }
                }
                else
                {
                    MessageBox.Show("Les identifiants saisit ne sont pas bon"); 
                }
            }
            
        }

        
    }
    }

