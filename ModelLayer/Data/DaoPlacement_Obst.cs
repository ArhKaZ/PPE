using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Business;

namespace ModelLayer.Data
{
    public class DaoPlacement_Obst
    {
        private Dbal thedbal;
        private DaoReservation theDaoReservation;
        private DaoObstacle theDaoObstacle;


        public DaoPlacement_Obst(Dbal mydabal, DaoReservation theDaoReservation, DaoObstacle theDaoObstacle)
        {
            thedbal = mydabal;
            this.theDaoReservation = theDaoReservation;
            this.theDaoObstacle = theDaoObstacle;
        }

        public void Insert(Placement_Obstacle thePlacementObs, Reservation theReservation, Obstacle theObstacle)
        {
            string query = "Placement_obstacle (num_placement, reservation, obstacle) VALUES ("
                + thePlacementObs.Num_placement + ","
                + theReservation.Id + ","
                + theObstacle.Id + ")";
            this.thedbal.Insert(query);
        }

        public List<Placement_Obstacle> SelectAll()
        {
            List<Placement_Obstacle> listPlacement_obs = new List<Placement_Obstacle>();
            DataTable myTable = this.thedbal.SelectAll("placement_obstacle");

            foreach (DataRow r in myTable.Rows)
            {
                Reservation myReservation = this.theDaoReservation.SelectbyId((int)r["id"]);
                Obstacle myObstacle = this.theDaoObstacle.SelectById((int)r["id"]);
                listPlacement_obs.Add(new Placement_Obstacle((int)r["num_placement"], myReservation, myObstacle));
            }
            return listPlacement_obs;
        }

        public void Update(Placement_Obstacle myPlacement_Obs, Reservation myReservation, Obstacle myObstacle)
        {
            string query = " Placement_obstacle Set num_placement=" + myPlacement_Obs.Num_placement
                + ", reservation = " + myReservation.Id
                + ", obstacle = " + myObstacle.Id
                + "WHERE num_placement =" + myPlacement_Obs.Num_placement;
            this.thedbal.Update(query);
        }

        public Placement_Obstacle SelectById(int id)
        {
            DataRow rowPlacement_obs = this.thedbal.SelectById("Placement_obstacle", id);
            Reservation myReservation = this.theDaoReservation.SelectbyId((int)rowPlacement_obs["id"]);
            Obstacle myObstacle = this.theDaoObstacle.SelectById((int)rowPlacement_obs["id"]);

            return new Placement_Obstacle((int)rowPlacement_obs["num_placement"], myReservation, myObstacle);
        }

        public void Delete(Placement_Obstacle unPlacement_obs)
        {
            string query = "Placement_obstacle where id= " + unPlacement_obs.Num_placement + ";";
            this.thedbal.Delete(query);
        }
    }
}
