using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Business
{
    public class Client
    {
        private int id;
        private string nom;
        private string prenom;
        private int telephone;
        private string mail;
        private int credit;
        private DateTime dateNaissance;
        private string photo;
        private int nbpartie;

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public int Telephone { get => telephone; set => telephone = value; }
        public string Mail { get => mail; set => mail = value; }
        public int Credit { get => credit; set => credit = value; }
        public DateTime DateNaissance { get => dateNaissance; set => dateNaissance = value; }
        public string Photo { get => photo; set => photo = value; }
        public int Nbpartie { get => nbpartie; set => nbpartie = value; }

        public Client(int id, string nom, string prenom, int telephone, string mail, int credit, DateTime dateNaissance, int nbpartie)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Telephone = telephone;
            Mail = mail;
            Credit = credit;
            DateNaissance = dateNaissance;
            Nbpartie = nbpartie;
        }
        
        public Client(){}

        public override string ToString()
        {
            return this.Nom + " " + this.Prenom;
        }

    }
}
