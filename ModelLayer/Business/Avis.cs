using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Data;

namespace ModelLayer.Business
{
    public class Avis
    {
        private int id;
        private Client idClient;
        private int note;
        private string commentaire;
        private Theme idTheme;

        public int Id { get => id; set => id = value; }
        public Client IdClient { get => idClient; set => idClient = value; }
        public int Note { get => note; set => note = value; }
        public string Commentaire { get => commentaire; set => commentaire = value; }
        public Theme IdTheme { get => idTheme; set => idTheme = value; }

        public Avis(int id, Client client, int note, string commentaire, Theme Theme)
        {
            Id = id;
            IdClient = client;
            Note = note;
            Commentaire = commentaire;
            IdTheme = Theme;
        }
        
        public Avis(){}

        
    }
}
