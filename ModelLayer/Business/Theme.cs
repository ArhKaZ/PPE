using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Business
{
   public class Theme
    {
        private int id;
        private string nom;

        public Theme(int id, string nom)
        {
            Id = id;
            Nom = nom;
        }
        
        public Theme(){}

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
    }
}
