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
        private Mechanics yPhysics { get; set; }
        private int toMove { get; set; }
        private int mvmt { get; set; }

        public Form1()
        {
            InitializeComponent();
            yPhysics = new Mechanics();
            toMove = 0;
            mvmt = 5;

            // Make the player a circle
            int subtract = 0;
            var gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, player.Width - subtract, player.Height - subtract);
            var rg = new Region(gp);
            player.Region = rg;
        }

        private bool atTop()
        {
            return (player.Top == ClientRectangle.Top) ? true : false;
        }
        private bool atBottom()
        {
            return (player.Bottom == ClientRectangle.Bottom) ? true : false;
        }
        private bool atLeft()
        {
            return (player.Left == ClientRectangle.Left) ? true : false;
        }
        private bool atRight()
        {
            return (player.Right == ClientRectangle.Right) ? true : false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (timer1.Enabled)
                {
                    timer1.Enabled = false;
                }
                else
                {
                    timer1.Enabled = true;
                }
            }
            if (e.KeyData == Keys.Space)
            {
                yPhysics.AccelApplied = -10;
            }
            if (e.KeyData == Keys.Right)
            {
                player.Left = Math.Min(ClientRectangle.Right - player.Width, player.Left + mvmt);
            }

            if (e.KeyData == Keys.Left)
            {
                player.Left = Math.Max(ClientRectangle.Left, player.Left - mvmt);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        { 
            if (!(atBottom() & yPhysics.AccelApplied == 0))
            {
                yPhysics.calcNewVelocity();
            }

            toMove = yPhysics.movePlayer();

            if (toMove < 0 & !atTop())
            {
                player.Top = Math.Max(ClientRectangle.Top, player.Top + toMove);
            }
            else if (!atBottom())
            {
                player.Top = Math.Min(ClientRectangle.Bottom - player.Height, player.Top + toMove);
            }

            if (atTop() || atBottom())
            {
                if (atBottom() & Math.Abs(yPhysics.Velocity) < 4)
                {
                    yPhysics.Velocity = 0;
                }
                else { yPhysics.elasticCollision(); }
            }
            yPhysics.AccelApplied = 0;
            label1.Text = "Vel:  " + (-yPhysics.Velocity).ToString();
        }
    }
}
