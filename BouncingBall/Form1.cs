using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form1
{
    public partial class Form1 : Form
    {
        private Mechanics Physics { get; set; }
        private List<PictureBox> ob { get; set; }
        private double time { get; set; }
        private int newObstacle { get; set; }

        public Form1()
        {
            InitializeComponent();
            Physics = new Mechanics();
            ob = new List<PictureBox>();
            time = 0;
            newObstacle = 0;

            // Make the player a circle
            int subtract = 0;
            var gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, player.Width - subtract, player.Height - subtract);
            var rg = new Region(gp);
            player.Region = rg;
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (timer1.Enabled)
                {
                    timer1.Stop();
                }
                else
                {
                    timer1.Start();
                }
            }
            if (e.KeyData == Keys.Space)
            {
                Physics.AccelApplied[1] = -10;
            }
            if (e.KeyData == Keys.Right)
            {
                Physics.AccelApplied[0] = 5;
            }

            if (e.KeyData == Keys.Left)
            {
                Physics.AccelApplied[0] = -5;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time += timer1.Interval / 1000.0;


            if (newObstacle == 80)
            {
                ob.Add(Physics.addObstacle());
                newObstacle = 0;
            }

            int[] newPositions = Physics.movePlayer(player);
            player.Left = newPositions[0];
            player.Top = newPositions[1];

            // Clear accelerations applied
            Physics.AccelApplied[0] = 0;
            Physics.AccelApplied[1] = 0;

            label1.Text = "VelX: " + Physics.Velocity[0] + "\nVelY: " + Physics.Velocity[1] + "\nTime: " + Math.Round(time, 1);

            // Moves the obstacles across bottom of screen
            for (int i = ob.Count - 1; i > -1; i--)
            {
                if (ob[i].Right <= ClientRectangle.Left)
                {
                    this.Controls.Remove(ob[i]);
                    ob.RemoveAt(i);
                }
                else { ob[i].Left -= 5; }
            }

            newObstacle++;
            
        }
    }
}

