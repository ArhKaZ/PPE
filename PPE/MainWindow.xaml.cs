using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ModelLayer.Business;
using ModelLayer.Data;
using PPE.viewModel;
namespace PPE
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(DaoAvis theDaoAvis,DaoClient theDaoClient ,DaoObstacle theDaoObstacle,DaoPlacement_Obst theDaoPObstacle,DaoReservation theDaoReservation,DaoSalle theDaoSalle,DaoTheme theDaoTheme,DaoTransaction theDaoTransaction,DaoUtilisateur theDaoUtilisateur,DaoVille theDaoVille )
        {
            //Finir le tuto : https://www.c-sharpcorner.com/UploadFile/mahesh/datagrid-in-wpf/#:~:text=WPF%20DataGrid.,display%20data%20from%20a%20collection.&text=In%20this%20article%2C%20you%20will,load%20data%20from%20a%20collection
            InitializeComponent();
            Grid_Clients.DataContext = new viewModelClient(theDaoClient);
            //Grid_Credits.DataContext = new viewModelCredits(theDaoClient, theDaoTransaction);

           // Grid_Credits.DataContext = new viewModelCredits();
        }
        
        
    }
}
