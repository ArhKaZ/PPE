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
using DevExpress.Mvvm;
using PPE.viewModel;
using ModelLayer.Business;
using ModelLayer.Data;
using PPE.inter_face;
namespace PPE
{
   

    /// <summary>
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Window { public bool IsClosed { get; set; } = false; public Connexion() { Closed += Connexion_Closed; InitializeComponent(); } } private void Connexion_Closed(object sender, EventArgs e) { isClosed = true; }, ICloseable
    {
        public Connexion(DaoUtilisateur thedaoUtilisateur)
        {
            InitializeComponent();
            
        }

        
    }

    
}
