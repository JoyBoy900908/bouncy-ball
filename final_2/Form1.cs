using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace final_2
{
    public partial class Form1 : Form
    {
        int speed = 40;
        int ballx = 10;//10
        int bally = 20;//20
        int sc,i;
        int brickRowCount, brickColumnCount, brickPadding, brickOffsetTop, brickOffsetLeft;
        bool gol=false, gor=false;
        PictureBox[] brick = new PictureBox[48];

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                gol = true;
            }
            if (e.KeyCode == Keys.D)
            {
                gor = true;
            }
        }

        private void ball_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                gol = false;
            }
            if (e.KeyCode == Keys.D)
            {
                gor = false;
            }
        }

        private Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gol && player.Left >=40)
                player.Left -= speed;
            if (gor && player.Left + player.Width <= 660)
                player.Left += speed;
            ball.Left += ballx;
            ball.Top += bally;

            if (ball.Left + ball.Width > 700 || ball.Left < 0)
            {
                ballx = -ballx; // this will bounce the object from the left or right border
            }

            if (ball.Top < 0 || ball.Bounds.IntersectsWith(player.Bounds))
            {
                bally = -bally; // this will bounce the object from top and bottom border
            }

            if (ball.Top > player.Top)
            {
                gameOver();
                System.Windows.Forms.MessageBox.Show("Game Over", "提示");
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "brick")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        bally = -bally;
                        sc++;
                    }
                }
            }
            score.Text = "score：" + sc;
            if (sc > 47)
            {
                gameOver();
                System.Windows.Forms.MessageBox.Show("Game Success", "提示");
            }
        }

        private void start_Click(object sender, EventArgs e)
        {
            int rnd = rand.Next(0, 2);
            if (rnd == 0)
                ballx *= -1;

            ball.Left = 330;
            ball.Top = 330;
            player.Left = 290;
            player.Top = 370;
            sc = 0;
            brickRowCount = 3; 
            brickColumnCount = 16;
            brickOffsetLeft = 10;//10
            brickOffsetTop = 50;//50
            brickPadding = 1;
            i = 0;
            for (int c = 0; c < brickColumnCount; c++)
            {
                for (int r = 0; r < brickRowCount; r++)
                {
                    brick[i] = new PictureBox();
                    brick[i].Size = new System.Drawing.Size(40, 25);
                    brick[i].Location = new Point((c * (45 + brickPadding)) + brickOffsetLeft, (r * (45+ brickPadding)) + brickOffsetTop);
                    brick[i].BringToFront();
                    if(r==0)
                        brick[i].BackColor = System.Drawing.Color.Yellow;
                    else if(r==1)
                        brick[i].BackColor = System.Drawing.Color.Blue;
                    else if (r == 2)
                        brick[i].BackColor = System.Drawing.Color.Green;
                        
                    brick[i].Tag = "brick";
                    this.Controls.Add(brick[i]);
                    i++;
                }
            }
            start.Enabled = false;
            start.Visible = false;
            label1.Visible = false;
            timer1.Start();
        }
        private void gameOver()
        {
            label1.Visible = true;
            start.Enabled = true;
            start.Visible = true;
            timer1.Stop();
        }
    }
}
