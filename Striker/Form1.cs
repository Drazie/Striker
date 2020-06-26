using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Striker
{
    public partial class Form1 : Form
    {
        
        bool goLeft;
        bool goRight;
        bool isGameOver = false;


        int score;
        int ballx;
        int bally;
        int playerSpeed;


        Random rnd = new Random();


        PictureBox[] blockArray;


        public Form1()
        {
            InitializeComponent();

            placeBlocks();
        }


        private void setupGame()
        {

            score = 0;
            playerSpeed = 13;
            ballx = 5;
            bally = 5;

            txtScore.Text = "Score: " + score;

            ball.Left = 724;
            ball.Top = 435;

            player.Left = 699;

            gameTimer.Start();




            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    x.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                }
            }
        }


        private void gameOver(string message)
        {
            isGameOver = true;
            gameTimer.Stop();
            txtScore.Text = "Score: " + score + " " + message;
        }


        private void placeBlocks()
        {

            blockArray = new PictureBox[24];

            int a = 0;
            int top = 50;
            int left = 60;

            for(int i = 0; i < blockArray.Length; i++)
            {
                blockArray[i] = new PictureBox();
                blockArray[i].Height = 32;
                blockArray[i].Width = 70;
                blockArray[i].Tag = "blocks";
                blockArray[i].BackColor = Color.White;


                if(a == 6)
                {
                    top += 50;
                    left = 60;
                    a = 0;
                }
                if(a < 6)
                {
                    a++;
                    blockArray[i].Left = left;
                    blockArray[i].Top = top;
                    this.Controls.Add(blockArray[i]);
                    left += 130;
                }
            }

            setupGame(); //restarts the game from inside

        }

        private void removeBlocks()
        {

            foreach(PictureBox x in blockArray)
            {
                this.Controls.Remove(x);
            }

        }




        private void Form1_Load(object sender, EventArgs e)
        {

        }




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void mainGsmeTimerEvent(object sender, EventArgs e)
        {

            txtScore.Text = "Score: " + score;

            if(goLeft == true && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }
            if(goRight == true && player.Left < 700)
            {
                player.Left += playerSpeed;
            }


            ball.Left += ballx;
            ball.Top += bally;

            if(ball.Left > 770 || ball.Left < 5)
            {
                ballx = -ballx;
            }
            if(ball.Top < 0)
            {
                bally = -bally;
            }
            if (ball.Bounds.IntersectsWith(player.Bounds))
            {
                ball.Top = 440;
                bally = rnd.Next(5, 12) * -1;
            }
            if(ballx < 0)
            {
                ballx = rnd.Next(5, 12) * -1;
            }
            else
            {
                ballx = rnd.Next(5, 12);
            }



            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "blocks")
                {
                    if(ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;
                        bally = -bally;

                        this.Controls.Remove(x);
                    }
                }
            }

            if(score == 24)
            {
                //show end game message here

                gameOver("You win! Press Enter to play again");
            }

            if(ball.Top > 548)
            {
                //game over
                gameOver("You lose:( Press Enter to try again");
            }









        }

        private void keyisdown(object sender, KeyEventArgs e)
        {

            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if(e.KeyCode == Keys.Right)
            {
                goRight = true;
            }

        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if(e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                removeBlocks();
                placeBlocks();
            }
        }
    }
}
