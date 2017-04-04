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
        private Random r { get; set; }
        private List<PictureBox> ob { get; set; }
        private double time { get; set; }
        private int obstacleCounter { get; set; }
        private int nextObstacle { get; set; }
        private bool gameOver { get; set; }
        private double highScore { get; set; }
        private int multipleMoves { get; set; }

        public Form1()
        {
            InitializeComponent();
            Physics = new Mechanics();
            r = new Random();
            ob = new List<PictureBox>();
            time = 0;
            obstacleCounter = 0;
            RestartNextObstacle();
            gameOver = false;
            highScore = 0;
            multipleMoves = 0;
            label1.Text = "Press Enter to Start";
            label2.Text = "High Score\n" + highScore;

            // Make the player a circle
            int subtract = 0;
            var gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, player.Width - subtract, player.Height - subtract);
            var rg = new Region(gp);
            player.Region = rg;

            TopBar.Width = ClientRectangle.Width;
        }

        private void AdjustLabels()
        {
            label2.Left = ClientRectangle.Right - label2.Width - 10;
            TopBar.Width = ClientRectangle.Width;
        }

        private void RestartNextObstacle()
        {
            nextObstacle = r.Next(20, 80);
        }

        private void RestartGame()
        {
            Physics.NewGameSettings();

            this.obstacleCounter = 0;
            this.RestartNextObstacle();

            player.Left = (ClientRectangle.Width + player.Width) / 2;
            player.Top = 30;
            foreach (var item in ob)
            {
                this.Controls.Remove(item);
            }
            ob.Clear();
            time = 0;
            gameOver = false;
            timer1.Stop();
            label1.Text = "Press Enter to Start";
            label2.Text = "High Score\n" + highScore;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.R) { RestartGame(); }

            if (e.KeyData == Keys.Enter & !gameOver)
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

            if (e.KeyData == Keys.Down)
            {
                Physics.AccelApplied[1] = 5;
            }
            else if (e.KeyData == Keys.Up)
            {
                Physics.AccelApplied[1] = -10;
            }

            if (e.KeyData == Keys.Right)
            {
                Physics.AccelApplied[0] = 5;
            }

            else if (e.KeyData == Keys.Left)
            {
                Physics.AccelApplied[0] = -5;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Checks to see if player is in contact with an obstacle
            if (Physics.inContact(player, ob, TopBar))
            {
                timer1.Stop();
                this.gameOver = true;
                label1.Text = "GAME OVER\nFINAL SCORE: " + time;
                if (time > highScore)
                {
                    highScore = time;
                    label1.Text += "\nNEW HIGH SCORE!";
                }

                return;
            }

            // Calculates new time
            time = Math.Round(time + (timer1.Interval / 1000.0), 2);

            // Adds a new obstacle if necessary
            if (obstacleCounter == nextObstacle)
            {
                ob.Add(Physics.addObstacle());
                obstacleCounter = 0;
                RestartNextObstacle();
            }

            // Moves the player
            int[] newPositions = Physics.movePlayer(player);
            player.Left = newPositions[0];
            player.Top = newPositions[1];

            // Clear accelerations applied
            Physics.AccelApplied[0] = 0;
            Physics.AccelApplied[1] = 0;

            // Displays velocities and time
            label1.Text = "Time: " + Math.Round(time, 1);

            // Moves the obstacles across bottom of screen
            for (int i = ob.Count - 1; i > -1; i--)
            {
                if (ob[i].Right <= ClientRectangle.Left)
                {
                    this.Controls.Remove(ob[i]);
                    ob.RemoveAt(i);
                }
                else
                {
                    ob[i].Left -= (5 + ((int)time/5));
                }
            }

            obstacleCounter++;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            AdjustLabels();
        }
    }
}
