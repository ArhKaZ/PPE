using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Business
{
    public class Reservation
    {

        private DateTime dateRes;
        private int id;
        private Client idClient;
        private Salle idSalle;
        private int prix;
        private Utilisateur idTechnicien;
        private int nbClient;
        private Theme idTheme;

        public Reservation(DateTime dateRes, int id, Client idClient, Salle idSalle, int prix, Utilisateur idTechnicien, int nbClient, Theme idTheme)
        {
            DateRes = dateRes;
            Id = id;
            IdClient = idClient;
            IdSalle = idSalle;
            Prix = prix;
            IdTechnicien = idTechnicien;
            NbClient = nbClient;
            IdTheme = idTheme;
        }

        public DateTime DateRes { get => dateRes; set => dateRes = value; }
        public int Id { get => id; set => id = value; }
        public Client IdClient { get => idClient; set => idClient = value; }
        public Salle IdSalle { get => idSalle; set => idSalle = value; }
        public int Prix { get => prix; set => prix = value; }
        public Utilisateur IdTechnicien { get => idTechnicien; set => idTechnicien = value; }
        public int NbClient { get => nbClient; set => nbClient = value; }
        public Theme IdTheme { get => idTheme; set => idTheme = value; }

        public Reservation() { }

        public override string ToString()
        {
            return this.dateRes.ToString() + "--" + this.idClient.Nom.ToString() + "--" + this.idClient.Prenom.ToString();
        }


    }
}
