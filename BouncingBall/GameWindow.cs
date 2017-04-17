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
    public partial class GameWindow : Form
    {
        private Mechanics Physics { get; set; }
        private Random r { get; set; }
        private List<PictureBox> ob { get; set; }
        private int score { get; set; }
        private int obstacleCounter { get; set; }
        private int nextObstacle { get; set; }
        private bool gameOver { get; set; }
        private bool paused { get; set; }
        private int[] highScore { get; set; }

        public GameWindow()
        {
            InitializeComponent();
            Physics = new Mechanics();
            r = new Random();
            ob = new List<PictureBox>();
            score = 0;
            obstacleCounter = 0;
            RestartNextObstacle();
            gameOver = false;
            highScore = new int[] { 0, 0 };
            AdjustLabels();

            // Make the player a circle
            int subtract = 0;
            var gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, player.Width - subtract, player.Height - subtract);
            var rg = new Region(gp);
            player.Region = rg;

            // Initializes start of game
            RestartGame();
        }

        private void StartGame()
        {
            GameClock.Start();
            MsgLabel.Text = "";
            OptionsLabel.Text = "";
            JustBounceLabel.Text = "";
            BounceNJumpLabel.Text = "";
        }

        private void PauseControl()
        {
            if (paused)
            {
                paused = false;
                StartGame();
            }
            else
            {
                paused = true;
                GameClock.Stop();
                MsgLabel.Text = "        PAUSED       \n\n(Space to Start)";
            }
        }

        private void RestartGame()
        {
            Physics.NewGameSettings();

            this.obstacleCounter = 0;
            this.RestartNextObstacle();

            player.Left = (ClientRectangle.Width - player.Width) / 2;
            player.Top = 40;
            foreach (var item in ob)
            {
                this.Controls.Remove(item);
            }
            ob.Clear();
            score = 0;
            gameOver = false;
            GameClock.Stop();
            ScoreLabel.Text = "Score: " + score.ToString("D8");
            SetHighScore();
            MsgLabel.Text = "Press Space to Start";
            OptionsLabel.Text = "Select Game Mode  \n('S' to switch)  ";
            JustBounceLabel.Text = "JustBounce";
            BounceNJumpLabel.Text = "BounceNJump";

            BoldGameMode();
        }

        private void GameOver()
        {
            GameClock.Stop();
            this.gameOver = true;
            int i = (Physics.BounceNJump) ? 1 : 0;
            MsgLabel.Text = "GAME OVER\nFINAL SCORE: " + score.ToString("D");
            if (score > highScore[i])
            {
                highScore[i] = score;
                MsgLabel.Text += "\nNEW HIGH SCORE!";
            }

            MsgLabel.Text += "\n\nPress Space to Restart";
        }

        private void AdjustLabels()
        {
            HighScoreLabel.Left = ClientRectangle.Right - HighScoreLabel.Width - 10;

            TopBar.Width = ClientRectangle.Width;

            MsgLabel.Left = (ClientRectangle.Width - MsgLabel.Width) / 2;
            MsgLabel.Top = ClientRectangle.Top + 125;

            OptionsLabel.Left = (ClientRectangle.Width - OptionsLabel.Width) / 2;
            OptionsLabel.Top = ClientRectangle.Bottom - 100;

            JustBounceLabel.Left = (ClientRectangle.Width - JustBounceLabel.Width) / 3;
            JustBounceLabel.Top = ClientRectangle.Bottom - 50;

            BounceNJumpLabel.Left = 2 * (ClientRectangle.Width - BounceNJumpLabel.Width) / 3;
            BounceNJumpLabel.Top = ClientRectangle.Bottom - 50;

            if (!GameClock.Enabled && !MsgLabel.Text.Contains("PAUSE"))
            {
                player.Left = (ClientRectangle.Width - player.Width) / 2;
            }
        }

        private void BoldGameMode()
        {
            if (Physics.BounceNJump)
            {
                BounceNJumpLabel.Font = new Font(BounceNJumpLabel.Font, FontStyle.Bold);
                JustBounceLabel.Font = new Font(JustBounceLabel.Font, FontStyle.Regular);
            }
            else
            {
                BounceNJumpLabel.Font = new Font(BounceNJumpLabel.Font, FontStyle.Regular);
                JustBounceLabel.Font = new Font(BounceNJumpLabel.Font, FontStyle.Bold);
            }
        }

        private void SetHighScore()
        {
            HighScoreLabel.Text = "High Score\n";
            HighScoreLabel.Text += (Physics.BounceNJump) ? highScore[1] : highScore[0];
        }

        private void RestartNextObstacle()
        {
            nextObstacle = r.Next(20, 50);
        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Space:
                    {
                        if (gameOver) { RestartGame(); }
                        else if (!paused && !GameClock.Enabled) { StartGame(); }
                        else { PauseControl(); }
                    } break;

                case Keys.S:
                    {
                        if (!string.IsNullOrEmpty(MsgLabel.Text))
                        {
                            Physics.ChangeGameMode();
                            BoldGameMode();
                            SetHighScore();
                        }
                    } break;

                // If not running, Esc closes the game //
                case Keys.Escape:
                    {
                        if (!GameClock.Enabled) { this.Close(); }
                    } break;

                // Allows one boost upwards in between wall/ground bounces //
                case Keys.Up:
                    {
                        if (Physics.BounceNJump && Physics.HasBounced)
                        {
                            Physics.AccelApplied[1] = -10;
                            Physics.HasBounced = false;
                        }
                    } break;

                // Unlimited movements to the right & left //
                case Keys.Right:
                    {
                        Physics.AccelApplied[0] = 2;
                    } break;

                case Keys.Left:
                    {
                        Physics.AccelApplied[0] = -2;
                    } break;
            }
        }

        private void GameWindow_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Right:
                    {
                        Physics.AccelApplied[0] = 0;
                    }
                    break;

                case Keys.Left:
                    {
                        Physics.AccelApplied[0] = 0;
                    }
                    break;
            }
        }

        private void GameClock_Tick(object sender, EventArgs e)
        {
            // Checks to see if player is in contact with an obstacle
            if (Physics.inContact(player, ob, TopBar))
            {
                GameOver();
                return;
            }

            // Calculates new time
            score += (int)Math.Round(GameClock.Interval / 10.0);

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

            // Clear vertical acceleration applied
            Physics.AccelApplied[1] = 0;

            // Displays velocities and time
            ScoreLabel.Text = string.Format("Score: {0}", score.ToString("D8"));

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
                    ob[i].Left -= 5;// (5 + ((int)score / 1000));
                }
            }

            obstacleCounter++;
            ScoreLabel.SendToBack();
            HighScoreLabel.SendToBack();
        }

        private void GameWindow_Resize(object sender, EventArgs e)
        {
            // If window is being resized, pause the game
            if (MsgLabel.Text == "")
            {
                GameClock.Stop();
                MsgLabel.Text = "        PAUSED       \n\n(Space to Restart)";
            }

            // Readjust the labels
            AdjustLabels();
        }
    }
}
