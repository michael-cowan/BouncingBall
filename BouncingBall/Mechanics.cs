using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using Form1;

namespace Form1
{
    class Mechanics
    {
        private int g { get; set; }
        private double collisionEnergyLost { get; set; }

        public double[] Velocity { get; set; }
        public int[] AccelApplied { get; set; }

        public Mechanics()
        {
            // Gravity acceleration: px / dt
            g = 2;

            // Energy lost in collision (%)
            collisionEnergyLost = 20;

            // Array[] { X, Y }
            Velocity = new double[] { 0, 0 };
            AccelApplied = new int[] { 0, 0 };
        }

        public void NewGameSettings()
        {
            Velocity[0] = 0;
            Velocity[1] = 0;
            AccelApplied[0] = 0;
            AccelApplied[1] = 0;
        }

        public void calcNewVelocity()
        {

        }

        public void elasticCollision(int i)
        {
            // Calculate new velocity from collision
            double newVel = -(Velocity[i] * ((100 - collisionEnergyLost) / 100));

            // If velocity is less than 3 px / dt, round down to 0
            if (Math.Abs(newVel) < 5) { newVel = 0; }
            Velocity[i] = newVel;
        }

        public int[] movePlayer(PictureBox player)
        {
            Rectangle window = Form1.ActiveForm.ClientRectangle;
            int[] ans = new int[] { player.Left, player.Top };

            //newVel = (a + g)dt + vel
            //   dt ~= 1
            // Account for acceleration or collision if at wall

            // X axis
            if (!(player.Right == window.Right || player.Left == window.Left) || (Velocity[0] == 0))
            {
                Velocity[0] = Math.Round(Velocity[0] + AccelApplied[0]);
                
                // Rounds X axis velocity to 0 if it is under 3 px / dt
                if (Math.Abs(Velocity[0]) < 3) { Velocity[0] = 0; }
            }
            else { elasticCollision(0); }

            // Y axis
            if (!(player.Top == window.Top || player.Bottom == window.Bottom) || (Velocity[1] == 0))
            {
                // Doesn't calculate new velocity if no force is applied and ball is on bottom of window
                if (!(player.Bottom == window.Bottom & Velocity[1] == 0 & AccelApplied[1] >= 0))
                {
                    Velocity[1] = Math.Round(AccelApplied[1] + g + Velocity[1]);
                }
            }
            else { elasticCollision(1); }

            

            // Find the new position of the player
            int[,] minMax = new int[,] { { window.Left, window.Right - player.Width }, { window.Top, window.Bottom - player.Height } };
            for (int i = 0; i < 2; i++)
            {
                // Makes sure player stays within window
                if (Velocity[i] < 0)
                {
                    ans[i] = (int)Math.Round(Math.Max(minMax[i, 0], ans[i] + Velocity[i]));
                }
                else
                {
                    ans[i] = (int)Math.Round(Math.Min(minMax[i, 1], ans[i] + Velocity[i]));
                }
            }

            return ans;
        }

        public PictureBox addObstacle()
        {
            Rectangle window = Form.ActiveForm.ClientRectangle;
            Random r = new Random();

            PictureBox p = new PictureBox();
            p.Name = "Obstacle";
            p.BackColor = Color.Blue;
            p.Size = new Size(r.Next(10, (int)window.Width/8), r.Next(10, (int)window.Height/3));
            p.Left = window.Right;
            p.Top = window.Bottom - p.Height;
            Form1.ActiveForm.Controls.Add(p);
            return p;
        }

        public bool inContact(PictureBox player, List<PictureBox> ob)
        {
            foreach (var item in ob)
            {
                if (player.Bounds.IntersectsWith(item.Bounds))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
