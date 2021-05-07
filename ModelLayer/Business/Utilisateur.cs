using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Business
{
    public class Utilisateur
    {
        private int id;
        private string roleUser;
        private Ville idVille;
        private string identifiant;
        private string mdp;

        public Utilisateur(int unid, string roleduUser, Ville uneVille, string unidentifiant, string unmdp)
        {
            id = unid;
            roleUser = roleduUser;
            idVille = uneVille;
            identifiant = unidentifiant;
            mdp = unmdp;
        }
        
        public Utilisateur(string unidentifiant, string unmdp)
        {
            identifiant = unidentifiant;
            mdp = unmdp;
        }

        public int Id { get => id; set => id = value; }
        public string RoleUser { get => roleUser; set => roleUser = value; }
        public Ville Ville { get => idVille; set => idVille = value; }
        public string Identifiant { get => identifiant; set => identifiant = value; }
        public string Mdp { get => mdp; set => mdp = value; }

       
    }
}
