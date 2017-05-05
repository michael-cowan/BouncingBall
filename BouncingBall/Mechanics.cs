using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace BouncingBall
{
    class Mechanics
    {
        // PROPERTIES //
        private double collisionEnergyLost { get; set; }
        private double[] g { get; set; }

        public double[] AccelApplied { get; set; }
        public bool BounceNJump { get; set; }
        public bool HasBounced { get; set; }
        public double[] Velocity { get; set; }


        // CONSTRUCTOR //
        public Mechanics()
        {
            // Gravity acceleration: px / dt //
            g = new double[] { 0, 2.0};

            // BoostMode means you can use a boost (up arrow) //
            BounceNJump = false;

            // Energy lost in collision (%) //
            collisionEnergyLost = 0;

            // Array[] { X, Y } //
            AccelApplied = new double[] { 0, 0 };
            Velocity = new double[] { 0, 0 };

            HasBounced = false;
        }


        // METHODS //
        public void ChangeGameMode()
        {
            BounceNJump = (BounceNJump) ? false : true;
            collisionEnergyLost = (BounceNJump) ? 20 : 0;
        }
        private void ElasticCollision(int i)
        {
            HasBounced = true;

            // Calculate new velocity from collision //
            double newVel = -(Velocity[i] * ((100 - collisionEnergyLost) / 100.0));

            // If velocity is less than 5 px / dt, round down to 0 //
            if (Math.Abs(newVel) < 5) { newVel = 0; }
            Velocity[i] = newVel;
        }
        public bool inContact(PictureBox player, List<PictureBox> ob, PictureBox TopBar)
        {
            if (player.Bounds.IntersectsWith(TopBar.Bounds)) { return true; }
            foreach (var item in ob)
            {
                if (player.Bounds.IntersectsWith(item.Bounds))
                {
                    return true;
                }
            }
            return false;
        }
        public int[] movePlayer(PictureBox player)
        {
            Rectangle window = GameWindow.ActiveForm.ClientRectangle;
            int[] position = new int[] { player.Left, player.Top };

            //newVel = (a + g)dt + vel //
            //   dt ~= 1    //
            // Account for acceleration or collision if at wall //

            // X axis
            if (!(player.Right == window.Right || player.Left == window.Left) || (Velocity[0] == 0))
            {
                Velocity[0] = Math.Round(Velocity[0] + AccelApplied[0] + g[0]);
            }
            else { ElasticCollision(0); }

            // Y axis
            if ((player.Bottom != window.Bottom && player.Top != window.Top) || (Velocity[1] == 0))
            {
                double total = position[1] + AccelApplied[1] + g[1] + Velocity[1];
                int lowest = window.Bottom - player.Height;
                if (total > lowest)
                {
                    Velocity[1] += Math.Ceiling((AccelApplied[1] + g[1]) * ((lowest - position[1]) / (total - position[1])));
                }
                else
                {
                    Velocity[1] = Math.Round(Velocity[1] + AccelApplied[1] + g[1]);
                }
            }
            else { ElasticCollision(1); }


            // Find the new position of the player //
            int[,] minMax = new int[,]
            { 
                { window.Left, window.Right - player.Width },
                { window.Top, window.Bottom - player.Height }
            };

            for (int i = 0; i < 2; i++)
            {
                // Makes sure player stays within window //
                if (Velocity[i] < 0)
                {
                    position[i] = (int)Math.Round(Math.Max(minMax[i, 0], position[i] + Velocity[i]));
                }
                else
                {
                    position[i] = (int)Math.Round(Math.Min(minMax[i, 1], position[i] + Velocity[i]));
                }
            }

            return position;
        }
        public void NewGameSettings()
        {
            Velocity[0] = 0;
            Velocity[1] = 0;
            AccelApplied[0] = 0;
            AccelApplied[1] = 0;
        }
    }
}