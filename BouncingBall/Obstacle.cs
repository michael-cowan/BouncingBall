using System;
using System.Drawing;
using System.Windows.Forms;

namespace BouncingBall
{
    class Obstacle : PictureBox
    {
        private const double percentOnTop = 0.4;
        private Random rand { get; }

        public Obstacle(Color color = default(Color))
        {
            this.SizeMode = PictureBoxSizeMode.Zoom;
            this.Name = "Obstacle";
            this.BackColor = Color.Blue;
            this.rand = new Random();
            this.Height = rand.Next(this.fracOfWindow(10, 'y'), this.fracOfWindow(4, 'y'));
            this.Width =  rand.Next(this.fracOfWindow(12, 'x'), this.fracOfWindow(10, 'x'));
            this.Left = Form.ActiveForm.ClientRectangle.Right;
            
            if (rand.NextDouble() < percentOnTop)
            {
                this.Top = Form.ActiveForm.ClientRectangle.Top;
            }
            else
            {
                this.Top = Form.ActiveForm.ClientRectangle.Bottom - this.Height;
            }

            Form.ActiveForm.Controls.Add(this);
        }

        private int fracOfWindow(int denominator, char direction = 'x')
        {
            if (direction == 'x')
            {
                return Form.ActiveForm.ClientRectangle.Width / denominator;
            }
            else
            {
                return Form.ActiveForm.ClientRectangle.Height / denominator;
            }
        }
    }
}
