using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using Form1;

namespace BouncingBall
{
    class Obstacle : PictureBox
    {
        private double percentOnTop { get; }
        private Random r { get; set; }

        public Obstacle(double _percentOnTop)
        {
            Form.ActiveForm.Controls.Add(this);
            
            this.Name = "Obstacle";
            this.BackColor = Color.Blue;
            this.percentOnTop = _percentOnTop;
            this.r = new Random();
            this.Width = r.Next(this.fracOfWindow(12), this.fracOfWindow(10));
            this.Height = r.Next(this.fracOfWindow(10, 'y'), this.fracOfWindow(4, 'y'));
            this.Left = Form.ActiveForm.ClientRectangle.Right;

            if (r.NextDouble() < this.percentOnTop)
            {
                this.Top = Form.ActiveForm.ClientRectangle.Top;
            }
            else
            {
                this.Top = Form.ActiveForm.ClientRectangle.Bottom - this.Height;
            }
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
