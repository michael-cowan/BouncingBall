namespace BouncingBall
{
    partial class GameWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameWindow));
            this.player = new System.Windows.Forms.PictureBox();
            this.GameClock = new System.Windows.Forms.Timer(this.components);
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.HighScoreLabel = new System.Windows.Forms.Label();
            this.TopBar = new System.Windows.Forms.PictureBox();
            this.MsgLabel = new System.Windows.Forms.Label();
            this.OptionsLabel = new System.Windows.Forms.Label();
            this.JustBounceLabel = new System.Windows.Forms.Label();
            this.BounceNJumpLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopBar)).BeginInit();
            this.SuspendLayout();
            // 
            // player
            // 
            this.player.BackColor = System.Drawing.Color.Maroon;
            this.player.Location = new System.Drawing.Point(309, 46);
            this.player.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(60, 62);
            this.player.TabIndex = 0;
            this.player.TabStop = false;
            // 
            // GameClock
            // 
            this.GameClock.Interval = 20;
            this.GameClock.Tick += new System.EventHandler(this.GameClock_Tick);
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.Location = new System.Drawing.Point(20, 20);
            this.ScoreLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(32, 20);
            this.ScoreLabel.TabIndex = 1;
            this.ScoreLabel.Text = "Vel";
            // 
            // HighScoreLabel
            // 
            this.HighScoreLabel.AutoSize = true;
            this.HighScoreLabel.Location = new System.Drawing.Point(620, 14);
            this.HighScoreLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.HighScoreLabel.Name = "HighScoreLabel";
            this.HighScoreLabel.Size = new System.Drawing.Size(88, 20);
            this.HighScoreLabel.TabIndex = 2;
            this.HighScoreLabel.Text = "High Score";
            this.HighScoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TopBar
            // 
            this.TopBar.BackColor = System.Drawing.Color.Blue;
            this.TopBar.Location = new System.Drawing.Point(0, 0);
            this.TopBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TopBar.Name = "TopBar";
            this.TopBar.Size = new System.Drawing.Size(726, 3);
            this.TopBar.TabIndex = 3;
            this.TopBar.TabStop = false;
            // 
            // MsgLabel
            // 
            this.MsgLabel.AutoSize = true;
            this.MsgLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MsgLabel.Location = new System.Drawing.Point(234, 158);
            this.MsgLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MsgLabel.Name = "MsgLabel";
            this.MsgLabel.Size = new System.Drawing.Size(195, 22);
            this.MsgLabel.TabIndex = 4;
            this.MsgLabel.Text = "Press Space to Start";
            this.MsgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OptionsLabel
            // 
            this.OptionsLabel.AutoSize = true;
            this.OptionsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptionsLabel.Location = new System.Drawing.Point(234, 445);
            this.OptionsLabel.Name = "OptionsLabel";
            this.OptionsLabel.Size = new System.Drawing.Size(178, 44);
            this.OptionsLabel.TabIndex = 5;
            this.OptionsLabel.Text = "Select Game Mode\r\n(\'S\' to switch)";
            this.OptionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JustBounceLabel
            // 
            this.JustBounceLabel.AutoSize = true;
            this.JustBounceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JustBounceLabel.Location = new System.Drawing.Point(175, 502);
            this.JustBounceLabel.Name = "JustBounceLabel";
            this.JustBounceLabel.Size = new System.Drawing.Size(104, 22);
            this.JustBounceLabel.TabIndex = 6;
            this.JustBounceLabel.Text = "JustBounce";
            this.JustBounceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BounceNJumpLabel
            // 
            this.BounceNJumpLabel.AutoSize = true;
            this.BounceNJumpLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BounceNJumpLabel.Location = new System.Drawing.Point(372, 502);
            this.BounceNJumpLabel.Name = "BounceNJumpLabel";
            this.BounceNJumpLabel.Size = new System.Drawing.Size(127, 22);
            this.BounceNJumpLabel.TabIndex = 7;
            this.BounceNJumpLabel.Text = "BounceNJump";
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 557);
            this.Controls.Add(this.BounceNJumpLabel);
            this.Controls.Add(this.JustBounceLabel);
            this.Controls.Add(this.OptionsLabel);
            this.Controls.Add(this.MsgLabel);
            this.Controls.Add(this.TopBar);
            this.Controls.Add(this.HighScoreLabel);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.player);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(739, 585);
            this.Name = "GameWindow";
            this.Text = " ";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameWindow_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameWindow_KeyUp);
            this.Resize += new System.EventHandler(this.GameWindow_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox player;
        private System.Windows.Forms.Timer GameClock;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Label HighScoreLabel;
        private System.Windows.Forms.PictureBox TopBar;
        private System.Windows.Forms.Label MsgLabel;
        private System.Windows.Forms.Label OptionsLabel;
        private System.Windows.Forms.Label JustBounceLabel;
        private System.Windows.Forms.Label BounceNJumpLabel;
    }
}

