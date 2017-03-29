using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form1
{
    class Mechanics
    {
        private double g { get; set; }
        private double collisionEnergyLost { get; set; }

        public double Velocity { get; set; }
        public double AccelApplied { get; set; }

        public Mechanics()
        {
            g = 2;

            // Energy lost to collision (%)
            collisionEnergyLost = 20;

            Velocity = 0;
            AccelApplied = 0;
        }

        public void calcNewVelocity()
        {
            // newVel = (a + g)dt + vel
            //  dt ~= 1
            Velocity = Math.Round(AccelApplied + g + Velocity);
        }

        public int movePlayer()
        {
            return (int)Math.Round(Velocity);
        }

        public void elasticCollision()
        {
            Velocity *= -((100 - collisionEnergyLost) / 100);
        }
    }
}
