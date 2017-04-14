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
        private double collisionEnergyLost { get; set; }
        private int g { get; set; }

        public int[] AccelApplied { get; set; }
        public bool BounceNJump { get; set; }
        public bool HasBounced { get; set; }
        public double percentFromTop { get; set; }
        public double[] Velocity { get; set; }

        public Mechanics()
        {
            // Gravity acceleration: px / dt
            g = 2;

            // BoostMode means you can use a boost (up arrow)
            BounceNJump = false;

            // Energy lost in collision (%)
            collisionEnergyLost = 0;

            // Array[] { X, Y }
            Velocity = new double[] { 0, 0 };
            AccelApplied = new int[] { 0, -2 };

            HasBounced = false;

            percentFromTop = 0.5;
        }

        public void ChangeGameMode()
        {
            BounceNJump = (BounceNJump) ? false : true;
            collisionEnergyLost = (BounceNJump) ? 20 : 0;
        }

        public void NewGameSettings()
        {
            Velocity[0] = 0;
            Velocity[1] = -2;
            AccelApplied[0] = 0;
            AccelApplied[1] = 0;
        }

        public void ElasticCollision(int i)
        {
            HasBounced = true;

            // Calculate new velocity from collision
            double newVel = -(Velocity[i] * ((100 - collisionEnergyLost) / 100));

            // If velocity is less than 3 px / dt, round down to 0
            if (Math.Abs(newVel) < 5) { newVel = 0; }
            Velocity[i] = newVel;
        }

        public int[] movePlayer(PictureBox player)
        {
            Rectangle window = GameWindow.ActiveForm.ClientRectangle;
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
            else { ElasticCollision(0); }

            // Y axis
            if (!(player.Top == window.Top || player.Bottom == window.Bottom) || (Velocity[1] == 0))
            {
                // Doesn't calculate new velocity if no force is applied and ball is on bottom of window
                if (!(player.Bottom == window.Bottom && Velocity[1] == 0 && AccelApplied[1] >= 0))
                {
                    Velocity[1] = Math.Round(AccelApplied[1] + g + Velocity[1]);
                }
            }
            else { ElasticCollision(1); }

            

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
            p.Size = new Size(r.Next(10, (int)window.Width/8), r.Next(30, (int)window.Height/3));
            p.Left = window.Right;

            // Determines whether obstacle appears on top or bottom of screen
            if (r.NextDouble() > percentFromTop)
            {
                p.Top = window.Bottom - p.Height;
            }
            else
            {
                p.Top = window.Top;
            }
            GameWindow.ActiveForm.Controls.Add(p);
            return p;
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
    }
}
