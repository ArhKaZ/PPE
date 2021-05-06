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
    public class DaoReservation
    {
        private Dbal mydbal;
        private DaoReservation theDaoReservation;
        private DaoClient theDaoClient;
        private DaoSalle theDaoSalle;
        private DaoUtilisateur theDaoUtilisateur;
        private DaoTheme theDaoTheme;

        public DaoReservation(Dbal dbal, DaoClient theDaoClient, DaoSalle theDaoSalle, DaoUtilisateur theDaoUtilisateur, DaoTheme theDaoTheme)
        {
            mydbal = dbal;
            this.theDaoClient = theDaoClient;
            this.theDaoSalle = theDaoSalle;
            this.theDaoTheme = theDaoTheme;
            this.theDaoUtilisateur = theDaoUtilisateur;
        }

        public void Insert(Reservation uneReservation)
        {
            string query = "Reservation (dateRes, id, idClient, idSalle, prix, idtechnicien, nbClient, idTheme) VALUES ('"
                + uneReservation.DateRes + "',"
                + uneReservation.Id + ","
                + uneReservation.IdClient.Id + ","
                + uneReservation.IdSalle.Id + ","
                + uneReservation.Prix + ","
                + uneReservation.IdTechnicien + ","
                + uneReservation.NbClient + ","
                + uneReservation.IdTheme + ")";

            this.mydbal.Insert(query);

        }

        public void Update(Reservation uneReservation)
        {
            string query = "Salle Set id= '" + uneReservation.DateRes
                + "', id = " + uneReservation.Id
                + ", idClient = " + uneReservation.IdClient.Id
                + ", idSalle = " + uneReservation.IdSalle.Id
                + ", prix = " + uneReservation.Prix
                + ", idTechnicien = " + uneReservation.IdTechnicien.Id
                + ", nbClient = " + uneReservation.NbClient
                + ", idTheme = " + uneReservation.IdTheme + ")";
            this.mydbal.Update(query);
        }

        public void Delete(Reservation uneReservation)
        {
            string query = "Reservation Where id = " + uneReservation.Id;
            this.mydbal.Delete(query);
        }

        public Reservation SelectbyId(int id)
        {
            DataRow rowReservation = this.mydbal.SelectById("resevation", id);
            Client unCli = this.theDaoClient.SelectById((int)rowReservation["idClient"]);
            Salle uneSalle = this.theDaoSalle.SelectById((int)rowReservation["idSalle"]);
            Utilisateur unUtilisateur = this.theDaoUtilisateur.SelectbyId((int)rowReservation["idTechnicien"]);
            Theme unTheme = this.theDaoTheme.SelectById((int)rowReservation["idTheme"]);
            return new Reservation((DateTime)rowReservation["dateRes"],
                (int)rowReservation["id"],
                unCli,
                uneSalle,
                (int)rowReservation["prix"],
                unUtilisateur,
                (int)rowReservation["nbClient"],
                unTheme);
        }

        public List<Reservation> SelectAll()
        {
            List<Reservation> listUtilisateur = new List<Reservation>();
            DataTable myTable = this.mydbal.SelectAll("Utilisateur");

            foreach (DataRow r in myTable.Rows)
            {
                Client unCli = this.theDaoClient.SelectById((int)r["idClient"]);
                Salle uneSalle = this.theDaoSalle.SelectById((int)r["idSalle"]);
                Utilisateur unUtilisateur = this.theDaoUtilisateur.SelectbyId((int)r["idTechnicien"]);
                Theme unTheme = this.theDaoTheme.SelectById((int)r["idTheme"]);
                listUtilisateur.Add(new Reservation((DateTime)r["dateRes"],
                (int)r["id"],
                unCli,
                uneSalle,
                (int)r["prix"],
                unUtilisateur,
                (int)r["nbClient"],
                unTheme));
            }
            return listUtilisateur;
        }
    }
}

