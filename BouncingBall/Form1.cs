﻿using System;
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

        public Form1()
        {
            InitializeComponent();
            Physics = new Mechanics();

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
                    timer1.Enabled = false;
                }
                else
                {
                    timer1.Enabled = true;
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
            int[] newPositions = Physics.movePlayer(player);
            player.Left = newPositions[0];
            player.Top = newPositions[1];

            // Clear accelerations applied
            Physics.AccelApplied[0] = 0;
            Physics.AccelApplied[1] = 0;

            label1.Text = "VelX: " + Physics.Velocity[0] + "\n" + "VelY: " + Physics.Velocity[1];
        }
    }
}
