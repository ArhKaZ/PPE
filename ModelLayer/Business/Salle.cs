using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Business
{
    public class Salle
    {
        private int id;
        private Ville idLieu;
        private Theme idTheme;

        public Salle(int id, Ville idLieu, Theme idTheme)
        {
            Id = id;
            IdLieu = idLieu;
            IdTheme = idTheme;
        }

        public Salle() { }

        public int Id { get => id; set => id = value; }
        public Ville IdLieu { get => idLieu; set => idLieu = value; }
        public Theme IdTheme { get => idTheme; set => idTheme = value; }

        public override string ToString()
        {
            return this.IdLieu.Nom + "--" + this.id.ToString();
        }
    }
}