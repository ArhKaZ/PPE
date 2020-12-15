using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Business
{
    public class Placement_Obstacle
    {
        private int num_placement;
        private Reservation reservation;
        private Obstacle obstacle;

        public int Num_placement { get => num_placement; set => num_placement = value; }
        internal Reservation Reservation { get => reservation; set => reservation = value; }
        internal Obstacle Obstacle { get => obstacle; set => obstacle = value; }


        public Placement_Obstacle(int num_placement, Reservation reservation, Obstacle obstacle)
        {
            Num_placement = num_placement;
            Reservation = reservation;
            Obstacle = Obstacle;
        }
        
        public Placement_Obstacle(){}

      
    }
}
