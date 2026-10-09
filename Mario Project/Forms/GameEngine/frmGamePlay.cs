using System;
using System.Drawing;
using System.Windows.Forms;
using MarioGameSystem.DAL;
using MarioGameSystem.Helpers;
using Timer = System.Windows.Forms.Timer;

namespace MarioGameSystem.Forms.GameEngine
{
    public partial class frmGamePlay : Form
    {
        private Timer gameLoopTimer = null!;
        private PictureBox mario = null!;
        private Label lblScore = null!, lblTime = null!, lblSound = null!;

        private bool goLeft, goRight, jumping;
        private int jumpSpeed = 12, gravity = 8, playerSpeed = 6;
        private int score = 0, timeRemaining = 100, ticks = 0;

        private ScoreManager scoreManager = null!;
        private SoundManager soundManager = null!;
        private string currentPlayer = string.Empty;

        public frmGamePlay(string playerName)
        {
            InitControls();
            try
            {
                currentPlayer = playerName;
                scoreManager = new ScoreManager("scores.txt");
                soundManager = new SoundManager("bgm.wav", "jump.wav");
                soundManager.PlayBGM();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Init error: " + ex.Message);
            }
        }

        private void InitControls()
        {
            this.ClientSize = new Size(800, 450);
            this.Text = "Super Mario Engine (.NET 10)";
            this.DoubleBuffered = true;
            this.KeyPreview = true;

            lblScore = new Label { Text = "Score: 0", Location = new Point(10, 10), AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };
            lblTime = new Label { Text = "Time: 100", Location = new Point(150, 10), AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };
            lblSound = new Label { Text = "🔊 Sound: ON", Location = new Point(680, 10), AutoSize = true, Cursor = Cursors.Hand };
            lblSound.Click += (s, e) => {
                soundManager.IsMuted = !soundManager.IsMuted;
                lblSound.Text = soundManager.IsMuted ? "🔇 Sound: OFF" : "🔊 Sound: ON";
            };

            mario = new PictureBox { Size = new Size(30, 40), BackColor = Color.Red, Location = new Point(50, 300) };

            this.Controls.AddRange(new Control[] { lblScore, lblTime, lblSound, mario });

            BuildPlatform(0, 380, 800, 70);
            BuildPlatform(200, 270, 120, 20);
            BuildCoin(240, 230);

            gameLoopTimer = new Timer { Interval = 20 };
            gameLoopTimer.Tick += GameLoop;
            gameLoopTimer.Start();

            this.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Left) goLeft = true;
                if (e.KeyCode == Keys.Right) goRight = true;
                if (e.KeyCode == Keys.Space && !jumping) { jumping = true; jumpSpeed = 12; }
            };

            this.KeyUp += (s, e) => {
                if (e.KeyCode == Keys.Left) goLeft = false;
                if (e.KeyCode == Keys.Right) goRight = false;
            };
        }

        private void BuildPlatform(int x, int y, int w, int h)
        {
            PictureBox p = new PictureBox { Location = new Point(x, y), Size = new Size(w, h), BackColor = Color.SaddleBrown, Tag = "platform" };
            this.Controls.Add(p);
        }

        private void BuildCoin(int x, int y)
        {
            PictureBox c = new PictureBox { Location = new Point(x, y), Size = new Size(20, 20), BackColor = Color.Gold, Tag = "coin" };
            this.Controls.Add(c);
        }

        private void GameLoop(object sender, EventArgs e)
        {
            try
            {
                ticks++;
                if (ticks % 50 == 0)
                {
                    timeRemaining--;
                    lblTime.Text = "Time: " + timeRemaining;
                    if (timeRemaining <= 0) EndGame("Time Expired!");
                }

                mario.Top += gravity;

                if (jumping)
                {
                    mario.Top -= jumpSpeed;
                    jumpSpeed -= 1;
                    if (jumpSpeed < 0) jumping = false;
                }

                if (goLeft && mario.Left > 0) mario.Left -= playerSpeed;
                if (goRight && mario.Left + mario.Width < this.ClientSize.Width) mario.Left += playerSpeed;

                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is PictureBox pb && (string)pb.Tag == "platform")
                    {
                        if (mario.Bounds.IntersectsWith(pb.Bounds) && !jumping) mario.Top = pb.Top - mario.Height;
                    }
                    if (ctrl is PictureBox coin && (string)coin.Tag == "coin" && coin.Visible)
                    {
                        if (mario.Bounds.IntersectsWith(coin.Bounds))
                        {
                            coin.Visible = false;
                            score += 10;
                            lblScore.Text = "Score: " + score;
                            soundManager.PlaySFX();
                        }
                    }
                }

                if (mario.Top > this.ClientSize.Height) EndGame("Mario Fell!");
            }
            catch (Exception ex)
            {
                gameLoopTimer.Stop();
                MessageBox.Show("Runtime Error: " + ex.Message);
            }
        }

        private void EndGame(string msg)
        {
            gameLoopTimer.Stop();
            soundManager.StopBGM();
            try { scoreManager.SaveScore(currentPlayer, score); } catch { }
            MessageBox.Show($"{msg}\nScore: {score}", "Game Over");
            this.Close();
        }
    }
}