using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ModelLayer.Business;
using ModelLayer.Data;
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
           
            theDaoClient = new DaoClient(mydbal);
            theDaoTheme = new DaoTheme(mydbal);
            theDaoVille = new DaoVille(mydbal);
            theDaoAvis = new DaoAvis(mydbal, theDaoClient, theDaoTheme);
            theDaoObstacle = new DaoObstacle(mydbal, theDaoTheme);
            theDaoSalle = new DaoSalle(mydbal, theDaoVille, theDaoTheme);
            
          
            
            theDaoUtilisateur = new DaoUtilisateur(mydbal, theDaoVille);
            theDaoReservation = new DaoReservation(mydbal, theDaoClient, theDaoSalle, theDaoUtilisateur, theDaoTheme);
            theDaoTransaction = new DaoTransaction(mydbal, theDaoClient, theDaoReservation);
            theDaoPObstacle = new DaoPlacement_Obst(mydbal, theDaoReservation, theDaoObstacle);
          //  Connexion wndCx = new Connexion(theDaoUtilisateur);
            wndCx.Show();
            //if (this.wndCx.)
          //  {
            //    wnd.Hide();
          //  }
        }
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }

       // public static bool IsWindowOpen<T>(string name = "") where T : Connexion
       // {
       //     return string.IsNullOrEmpty(name)
             //   ? Application.Current.Windows.OfType<T>.Any();
       // }
    }
}
