using Project2.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        enum enWinner { Player1, Player2, Draw, InProgress}
        enum enPlayer { Player1, Player2};

        struct stGameStatus
        {
            public enWinner Winner;
            public short PlayCount;
            public bool GameOver;
        };

        enPlayer PlayerTurn = enPlayer.Player1;
        stGameStatus GameStatus;


        void EndGame()
        {
            LblPlayerTurn.Text = "Game Over";
            switch(GameStatus.Winner)
            {
                case enWinner.Player1:
                    LblPlayerWinner.Text = "Player 1";
                    break;
                case enWinner.Player2:
                    LblPlayerWinner.Text = "Player 2";
                    break;
                default:
                    LblPlayerWinner.Text = "Draw";
                    break;
            }

            MessageBox.Show("Game Over", "GameOver", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        bool CheckValues(PictureBox PB1,  PictureBox PB2, PictureBox PB3)
        {
            if(PB1.Tag.ToString() != "?" && PB1.Tag.ToString() == PB2.Tag.ToString() && PB2.Tag.ToString() == PB3.Tag.ToString())
            {
                PB1.BackColor = Color.Lime;
                PB2.BackColor = Color.Lime;
                PB3.BackColor = Color.Lime;

                if(PB1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;

                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                }

                GameStatus.GameOver = true;
                EndGame();
                return true;
            }

            GameStatus.GameOver = false;
            return false;
        }
        void CheckForWinner()
        {
            if (CheckValues(pictureBox1, pictureBox2, pictureBox3))
                return;
            
            if (CheckValues(pictureBox4, pictureBox5, pictureBox6))
                return;
           
            if (CheckValues(pictureBox7, pictureBox8, pictureBox9))
                return;
               
            if (CheckValues(pictureBox1, pictureBox4, pictureBox7))
                return;
                 
            if (CheckValues(pictureBox2, pictureBox5, pictureBox8))
                return;

            if (CheckValues(pictureBox3, pictureBox6, pictureBox9))
                return;

            if (CheckValues(pictureBox1, pictureBox5, pictureBox9))
                return;

            if (CheckValues(pictureBox3, pictureBox5, pictureBox7))
                return;

        }
        void ChangeImage(PictureBox PB)
        {

            if (PB.Tag.ToString() == "?")
            {
                switch (PlayerTurn)
                {
                    case enPlayer.Player1:

                        PB.Image = Resources.X;
                        PB.Tag = "X";
                        PlayerTurn = enPlayer.Player2;
                        LblPlayerTurn.Text = "Player 2";
                        LblPlayerTurn.Tag = "O";
                        GameStatus.PlayCount++;
                        CheckForWinner();
                        break;

                    case enPlayer.Player2:

                        PB.Image = Resources.O;
                        PB.Tag = "O";
                        PlayerTurn = enPlayer.Player1;
                        LblPlayerTurn.Text = "Player 1";
                        LblPlayerTurn.Tag = "X";
                        GameStatus.PlayCount++;
                        CheckForWinner();
                        break;

                }

            }
            else
            {
                MessageBox.Show("Wrong Choice", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if(GameStatus.PlayCount == 9 && !GameStatus.GameOver)
            {
                GameStatus.GameOver = true;
                GameStatus.Winner = enWinner.Draw;
                EndGame();
            }

        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            ChangeImage((PictureBox)sender);
        }

        void ResetPB(PictureBox PB)
        {
            PB.Image = Resources.question_mark_96;
            PB.Tag = "?";
            PB.BackColor = Color.Black;
        }
        void RestartGame()
        {
            ResetPB(pictureBox1);
            ResetPB(pictureBox2);
            ResetPB(pictureBox3);
            ResetPB(pictureBox4);
            ResetPB(pictureBox5);
            ResetPB(pictureBox6);
            ResetPB(pictureBox7);
            ResetPB(pictureBox8);
            ResetPB(pictureBox9);

            PlayerTurn = enPlayer.Player1;
            LblPlayerTurn.Text = "Player 1";
            LblPlayerTurn.Tag = "X";

            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            GameStatus.Winner = enWinner.InProgress;
            LblPlayerWinner.Text = "In Progress";
        }
        private void BtnRestartGame_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.White);
            pen.Width = 5;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(pen, 150, 200, 450, 200); // row line
            e.Graphics.DrawLine(pen, 250, 100, 250, 400); // column line


            e.Graphics.DrawLine(pen, 150, 300, 450, 300); // second row line
            e.Graphics.DrawLine(pen, 350, 100, 350, 400); // second column line
        }

    }

}
