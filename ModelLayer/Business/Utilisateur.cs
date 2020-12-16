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
        private string role;
        private Ville idVille;
        private string identifiant;
        private string mdp;

        public Utilisateur(int id, string roleUser, Ville uneVille, string identifiant, string mdp)
        {
            Id = id;
            Role = roleUser;
            idVille = uneVille;
            Identifiant = identifiant;
            Mdp = mdp;
        }
        
        public Utilisateur(){}

        public int Id { get => id; set => id = value; }
        public string Role { get => role; set => role = value; }
        public Ville Ville { get => idVille; set => idVille = value; }
        public string Identifiant { get => identifiant; set => identifiant = value; }
        public string Mdp { get => mdp; set => mdp = value; }

       
    }
}
