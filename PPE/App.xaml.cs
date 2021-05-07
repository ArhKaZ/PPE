using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ModelLayer.Business;
using ModelLayer.Data;
using PPE.viewModel;
using CommonServiceLocator;
namespace PPE
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Dbal mydbal;
        private DaoAvis theDaoAvis;
        private DaoClient theDaoClient;
        private DaoObstacle theDaoObstacle;
        private DaoPlacement_Obst theDaoPObstacle;
        private DaoReservation theDaoReservation;
        private DaoSalle theDaoSalle;
        private DaoTheme theDaoTheme;
        private DaoTransaction theDaoTransaction;
        private DaoUtilisateur theDaoUtilisateur;
        private DaoVille theDaoVille; 

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            mydbal = new Dbal("escp_Game");
            theDaoAvis = new DaoAvis(mydbal, theDaoClient, theDaoTheme);
            theDaoClient = new DaoClient(mydbal);
            theDaoObstacle = new DaoObstacle(mydbal, theDaoTheme);
            theDaoPObstacle = new DaoPlacement_Obst(mydbal, theDaoReservation, theDaoObstacle);
            theDaoReservation = new DaoReservation(mydbal, theDaoClient, theDaoSalle, theDaoUtilisateur, theDaoTheme);
            theDaoSalle = new DaoSalle(mydbal, theDaoVille, theDaoTheme);
            theDaoTheme = new DaoTheme(mydbal);
            theDaoTransaction = new DaoTransaction(mydbal, theDaoClient, theDaoReservation);
            theDaoVille = new DaoVille(mydbal);
            theDaoUtilisateur = new DaoUtilisateur(mydbal, theDaoVille);
            
            Connexion wndco = new Connexion(theDaoUtilisateur, theDaoVille);
            wndco.Show();
            if (wndco.IsActive == false)
            {
                MainWindow wnd = new MainWindow(theDaoAvis, theDaoClient, theDaoObstacle, theDaoPObstacle, theDaoReservation, theDaoSalle, theDaoTheme, theDaoTransaction, theDaoVille);
                wnd.Show();
            }
        }
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }
    }
}
