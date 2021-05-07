using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Business;
using System.Data;
using System.Runtime.CompilerServices;
using System.IO;
using System.Globalization;

namespace ModelLayer.Data
{
    public class DaoUtilisateur
    {
        private Dbal mydbal;
        private DaoVille theDaoVille;

        public DaoUtilisateur(Dbal dbal, DaoVille unDaoVille)
        {
            mydbal = dbal;
            theDaoVille = unDaoVille;
        }

        public void Insert(Utilisateur unUser)
        {
            string query = "Utilisateur (id, roleUser, idVille, identifiant, mdp) VALUES ("
                + unUser.Id + ",'"
                + unUser.RoleUser + "',"
                + unUser.Ville.Id + ",'"
                + unUser.Identifiant.Replace("'", "''") + ","
                + unUser.Mdp.Replace("'", "''") + ")";
            this.mydbal.Insert(query);

        }

        public void Update(Utilisateur unUser)
        {
            string query = "Utilisateur Set id= " + unUser.Id 
                + ", role = '" + unUser.RoleUser
                + ", ville = " + unUser.Ville.Id
                + ", identifiant = '" + unUser.Identifiant.Replace("'", "''")
                + ", mdp = '" + unUser.Mdp.Replace("'", "''");
            this.mydbal.Update(query);
        }

        public void Delete(Utilisateur unUser)
        {
            string query = "Utilisateur Where id = " + unUser.Id;
            this.mydbal.Delete(query);
        }

        public List<Utilisateur> SelectAll()
        {
            List<Utilisateur> listUtilisateur = new List<Utilisateur>();
            DataTable myTable = this.mydbal.SelectAll("Utilisateur");

            foreach (DataRow r in myTable.Rows)
            {
                int id = (int)r["id"];
                string role = (string)r["roleUser"];
                string ide = (string)r["identifiant"];
                string mdp = (string)r["mdp"];
                Ville maVille = this.theDaoVille.SelectbyId((int)r["idVille"]);
                listUtilisateur.Add(new Utilisateur(
                (int)r["id"],
                 (string)r["roleUser"],
                 maVille,
                 (string)r["identifiant"],
                 (string)r["mdp"]));
            }
            return listUtilisateur;
        }

        public Utilisateur SelectbyId(int id)
        {
            DataRow rowUtilisateur = this.mydbal.SelectById("utilisateur", id);
            Ville maVille = this.theDaoVille.SelectbyId((int)rowUtilisateur["idVille"]);
            return new Utilisateur((int)rowUtilisateur["id"],
                (string)rowUtilisateur["roleUser"],
                maVille,
                (string)rowUtilisateur["identifiant"],
                (string)rowUtilisateur["mdp"]); 
        }
    }
}
