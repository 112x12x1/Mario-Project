using System;
using System.Collections.Generic;
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
        private PictureBox mario = null!, flagGoal = null!;
        private Label lblScore = null!, lblTime = null!, lblSound = null!, lblLevel = null!;

        private bool goLeft, goRight, jumping, isGrounded;
        private int jumpSpeed = 16, jumpForce = 16, gravity = 12, playerSpeed = 7;
        private int score = 0, timeRemaining = 60, ticks = 0;
        private int cameraOffsetX = 0;

        private ScoreManager scoreManager = null!;
        private SoundManager soundManager = null!;
        private string currentPlayer = string.Empty;
        private LevelConfig currentLevelConfig = null!;
        private List<EnemyControl> enemyList = new List<EnemyControl>();

        public frmGamePlay(string playerName, int levelNumber = 1)
        {
            currentPlayer = playerName;
            currentLevelConfig = LevelRepository.GetLevel(levelNumber);
            timeRemaining = currentLevelConfig.TimeLimit;

            InitControls();

            try
            {
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
            this.Text = $"Super Mario - {currentLevelConfig.LevelName}";
            this.DoubleBuffered = true;
            this.KeyPreview = true;

            lblLevel = new Label { Text = currentLevelConfig.LevelName, Location = new Point(10, 10), AutoSize = true, Font = new Font("Arial", 11, FontStyle.Bold) };
            lblScore = new Label { Text = "Score: 0", Location = new Point(300, 10), AutoSize = true, Font = new Font("Arial", 11, FontStyle.Bold) };
            lblTime = new Label { Text = $"Time: {timeRemaining}", Location = new Point(440, 10), AutoSize = true, Font = new Font("Arial", 11, FontStyle.Bold) };
            lblSound = new Label { Text = "🔊 Sound: ON", Location = new Point(670, 10), AutoSize = true, Cursor = Cursors.Hand, Font = new Font("Arial", 11, FontStyle.Bold) };

            lblSound.Click += (s, e) => {
                soundManager.IsMuted = !soundManager.IsMuted;
                lblSound.Text = soundManager.IsMuted ? "🔇 Sound: OFF" : "🔊 Sound: ON";
            };

            mario = new PictureBox { Size = new Size(30, 40), BackColor = Color.Red, Location = currentLevelConfig.StartPoint };
            flagGoal = new PictureBox { Size = new Size(25, 40), BackColor = Color.Green, Location = currentLevelConfig.GoalPoint, Tag = "goal" };

            this.Controls.AddRange(new Control[] { lblLevel, lblScore, lblTime, lblSound, mario, flagGoal });

            foreach (var p in currentLevelConfig.Platforms)
            {
                BuildPlatform(p.Bounds.X, p.Bounds.Y, p.Bounds.Width, p.Bounds.Height, p.Color);
            }

            foreach (var c in currentLevelConfig.Coins)
            {
                BuildCoin(c.Location.X, c.Location.Y);
            }

            foreach (var e in currentLevelConfig.Enemies)
            {
                EnemyControl enemy = new EnemyControl(e.Location, e.MoveRange, e.Speed);
                enemyList.Add(enemy);
                this.Controls.Add(enemy.Picture);
            }

            gameLoopTimer = new Timer { Interval = 20 };
            gameLoopTimer.Tick += GameLoop;
            gameLoopTimer.Start();

            this.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Left) goLeft = true;
                if (e.KeyCode == Keys.Right) goRight = true;
                if ((e.KeyCode == Keys.Space || e.KeyCode == Keys.Up) && isGrounded && !jumping)
                {
                    jumping = true;
                    isGrounded = false;
                    jumpSpeed = jumpForce;
                }
            };

            this.KeyUp += (s, e) => {
                if (e.KeyCode == Keys.Left) goLeft = false;
                if (e.KeyCode == Keys.Right) goRight = false;
            };
        }

        private void BuildPlatform(int x, int y, int w, int h, Color color)
        {
            PictureBox p = new PictureBox { Location = new Point(x, y), Size = new Size(w, h), BackColor = color, Tag = "platform" };
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
                    if (timeRemaining <= 0) EndGame("Time Expired!", false);
                }

                // Di chuyển di động Mario & Cuộn Camera
                if (goLeft && mario.Left > 0)
                {
                    mario.Left -= playerSpeed;
                }
                if (goRight)
                {
                    if (mario.Left < 400 || cameraOffsetX >= currentLevelConfig.MapWidth - this.ClientSize.Width)
                    {
                        if (mario.Left + mario.Width < this.ClientSize.Width) mario.Left += playerSpeed;
                    }
                    else
                    {
                        // Cuộn màn hình (Camera Offset)
                        int moveShift = playerSpeed;
                        cameraOffsetX += moveShift;

                        foreach (Control ctrl in this.Controls)
                        {
                            if (ctrl != mario && ctrl != lblLevel && ctrl != lblScore && ctrl != lblTime && ctrl != lblSound)
                            {
                                ctrl.Left -= moveShift;
                            }
                        }

                        foreach (var enemy in enemyList)
                        {
                            enemy.StartX -= moveShift;
                        }
                    }
                }

                // Xử lý nhảy & Trọng lực
                if (jumping)
                {
                    mario.Top -= jumpSpeed;
                    jumpSpeed -= 1;
                    if (jumpSpeed <= 0) jumping = false;
                }
                else
                {
                    mario.Top += gravity;
                }

                isGrounded = false;

                // Xử lý Kẻ địch
                foreach (var enemy in enemyList)
                {
                    if (!enemy.IsAlive) continue;

                    enemy.UpdateMovement();

                    if (mario.Bounds.IntersectsWith(enemy.Picture.Bounds))
                    {
                        // Kiểm tra giẫm lên đầu (Mario đang rơi xuống)
                        if (!jumping && mario.Top + mario.Height - gravity <= enemy.Picture.Top + 12)
                        {
                            enemy.IsAlive = false;
                            enemy.Picture.Visible = false;
                            score += 20;
                            lblScore.Text = "Score: " + score;
                            soundManager.PlaySFX();

                            // Mario nẩy lên nhẹ khi tiêu diệt quái
                            jumping = true;
                            jumpSpeed = 10;
                        }
                        else
                        {
                            EndGame("Mario đã bị Kẻ Địch tiêu diệt!", false);
                            return;
                        }
                    }
                }

                // Va chạm Sàn & Coin
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is PictureBox pb && (string)pb.Tag == "platform")
                    {
                        if (mario.Bounds.IntersectsWith(pb.Bounds))
                        {
                            if (!jumping && mario.Top + mario.Height - gravity <= pb.Top + 10)
                            {
                                mario.Top = pb.Top - mario.Height;
                                isGrounded = true;
                            }
                        }
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

                if (mario.Bounds.IntersectsWith(flagGoal.Bounds))
                {
                    EndGame($"VICTORY! Bạn đã vượt qua {currentLevelConfig.LevelName}!", true);
                }

                if (mario.Top > this.ClientSize.Height) EndGame("Mario rơi xuống vực!", false);
            }
            catch (Exception ex)
            {
                gameLoopTimer.Stop();
                MessageBox.Show("Runtime Error: " + ex.Message);
            }
        }

        private void EndGame(string msg, bool isWin)
        {
            gameLoopTimer.Stop();
            soundManager.StopBGM();
            try { scoreManager.SaveScore(currentPlayer, score); } catch { }
            MessageBox.Show($"{msg}\nTổng điểm: {score}", isWin ? "Chiến Thắng" : "Game Over");
            this.Close();
        }
    }
}