    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using WMPLib;

namespace FlappyBird
    {
        public partial class Form1 : Form
        {
            int pipeSpeed = 5;
            int gravity = 15;
            int score = 0;
            bool itemAppeared = false;
            bool itemCollected = false;
            bool fireBullet = false;
            int bulletCount = 0;
            private Timer bulletTimer;
            private Timer itemRespawnTimer;
            private Timer itemOutOfBoundsTimer;
            int bossSpeed = 5;
            int bulletHitCount = 0;
            int boss2HitCount = 0;
            int boss3HitCount = 0;
            private bool isPaused = false;
            private Label winLabel;

            private readonly WindowsMediaPlayer backgroundMusic = new WindowsMediaPlayer();
        public Form1()
            {

                InitializeComponent();
                pictureBox11.Visible = false;
                pictureBox10.Visible = false; 

                bulletTimer = new Timer();
                bulletTimer.Interval = 200; 
                bulletTimer.Tick += BulletTimer_Tick;

                itemRespawnTimer = new Timer();
                itemRespawnTimer.Interval = 7000;
                itemRespawnTimer.Tick += ItemRespawnTimer_Tick;

                itemOutOfBoundsTimer = new Timer();
                itemOutOfBoundsTimer.Interval = 7000;
                itemOutOfBoundsTimer.Tick += ItemOutOfBoundsTimer_Tick;
                PlayBackgroundMusic();

                winLabel = new Label();
                winLabel.Text = "YOU WIN";
                winLabel.Font = new Font("Pixel", 48, FontStyle.Bold); 
                winLabel.ForeColor = Color.White;
                winLabel.AutoSize = true;
                winLabel.TextAlign = ContentAlignment.MiddleCenter; 
                winLabel.BackColor = Color.Transparent; 
                winLabel.Visible = false; 
                this.Controls.Add(winLabel); 

                groupBox1.Visible = false;

        }
        private void PlayBackgroundMusic()
        {
            try
            {
                backgroundMusic.URL = "D:\\LeChiThanh_2122110144\\FlappyBird\\FlappyBird\\audio\\nhacnen.mp3";
                backgroundMusic.settings.volume = 100;
                backgroundMusic.settings.setMode("loop", true);
                backgroundMusic.controls.play(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi phát nhạc: " + ex.Message);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            backgroundMusic.controls.stop(); 
            base.OnFormClosing(e);
        }
        private void gametime_Tick(object sender, EventArgs e)
        {
            flappyBird.Top += gravity;
            pictureBox1.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            pictureBox2.Left -= pipeSpeed;
            pictureBox4.Left -= pipeSpeed;
            pictureBox9.Left -= pipeSpeed;
            pictureBox8.Left -= pipeSpeed;
            pictureBox13.Left -= pipeSpeed;
            pictureBox14.Left -= pipeSpeed;

            if (pictureBox11.Visible)
            {
                pictureBox11.Left -= pipeSpeed;
            }

            
            if (score >= 10)
            {
                pictureBox12.Top += bossSpeed;
                if (pictureBox12.Top < 0 || pictureBox12.Top > ClientSize.Height - pictureBox12.Height)
                {
                    bossSpeed = -bossSpeed;
                }
                pictureBox12.Visible = true;
            }
            else
            {
                pictureBox12.Visible = false; 
            }
            if (score >= 47)
            {
                pictureBox3.Top += bossSpeed;
                if (pictureBox3.Top < 0 || pictureBox3.Top > ClientSize.Height - pictureBox3.Height)
                {
                    bossSpeed = -bossSpeed; 
                }
                pictureBox3.Visible = true;
            }
            else
            {
                pictureBox3.Visible = false; 
            }
            if (score >= 95)
            {
                pictureBox5.Top += bossSpeed;
                if (pictureBox5.Top < 0 || pictureBox5.Top > ClientSize.Height - pictureBox5.Height)
                {
                    bossSpeed = -bossSpeed;
                }
                pictureBox5.Visible = true; 
            }
            else
            {
                pictureBox5.Visible = false;
            }
            scoreText.Text = "Score: " + score;
            UpdatePipesAndCheckCollision();
            CheckItemCollection();
            if (score >= 10 && !itemAppeared)
            {
                pictureBox11.Visible = true;
                pictureBox11.Location = GetRandomItemPosition();
                pictureBox11.BringToFront();
                itemAppeared = true;
            }
            if (itemAppeared && pictureBox11.Left < -pictureBox11.Width)
            {
                itemOutOfBoundsTimer.Start();
                itemAppeared = false;
            }
            if (score > 10)
            {
                pipeSpeed = 10;
            }
            if (score > 30)
            {
                pipeSpeed = 15;
            }
            if (score > 50)
            {
                pipeSpeed = 20;
            }
            if (score > 95)
            {
                pipeSpeed = 25;
            }
        }
        private void UpdatePipesAndCheckCollision()
            {
                if (pictureBox1.Left < -50)
                {
                    pictureBox1.Left = 800;
                    score++;
                }
                if (pipeTop.Left < -80)
                {
                    pipeTop.Left = 900;
                    score++;
                }
                if (pictureBox2.Left < -80)
                {
                    pictureBox2.Left = 950;
                    score++;
                }
                if (pictureBox4.Left < -80)
                {
                    pictureBox4.Left = 990;
                    score++;
                }
                if (pictureBox9.Left < -80)
                {
                    pictureBox9.Left = 1050;
                    score++;
                }
                if (pictureBox8.Left < -80)
                {
                    pictureBox8.Left = 1000;
                    score++;
                }
                if (pictureBox13.Left < -80)
                {
                    pictureBox13.Left = 1100;
                    score++;
                }
                if (pictureBox14.Left < -80)
                {
                    pictureBox14.Left = 1100;
                    score++;
                }

            if (flappyBird.Bounds.IntersectsWith(pictureBox1.Bounds) ||
                    flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                    flappyBird.Bounds.IntersectsWith(pictureBox2.Bounds) ||
                    flappyBird.Bounds.IntersectsWith(pictureBox4.Bounds) ||
                    flappyBird.Bounds.IntersectsWith(pictureBox9.Bounds) ||
                    flappyBird.Bounds.IntersectsWith(pictureBox8.Bounds) ||
                    flappyBird.Bounds.IntersectsWith(pictureBox13.Bounds) ||
                    flappyBird.Bounds.IntersectsWith(pictureBox14.Bounds) ||
                    flappyBird.Top < -20 || flappyBird.Bottom > ClientSize.Height)
                {
                    endGame();
                }
            }
            private void CheckItemCollection()
            {
                if (pictureBox11.Visible && flappyBird.Bounds.IntersectsWith(pictureBox11.Bounds) && !itemCollected)
                {
                    pictureBox11.Visible = false;
                    pictureBox10.Visible = true;
                    pictureBox10.Location = new Point(flappyBird.Left + 50, flappyBird.Top);
                    fireBullet = true;
                    itemCollected = true;
                    bulletCount = 0;
                    bulletTimer.Start();
                    itemRespawnTimer.Start();
                }
                if (itemCollected && itemAppeared)
                {
                    itemAppeared = false;
                }
            }
            private Point GetRandomItemPosition()
            {
                Random random = new Random();
                int x = ClientSize.Width;
                int y = random.Next(60, ClientSize.Height - pictureBox11.Height - 50);
                return new Point(x, y);
            }

            private void BulletTimer_Tick(object sender, EventArgs e)
            {
                if (fireBullet && pictureBox10.Visible)
                {
                    if (bulletCount < 5) 
                    {
                        pictureBox10.Left += 50;

                        if (pictureBox10.Left > ClientSize.Width)
                        {
                            ResetBullet();
                        }

                        if (CheckBulletCollision())
                        {
                            ResetBullet();
                        }
                    }
                    else
                    {
                        fireBullet = false;
                        pictureBox10.Visible = false;
                        bulletTimer.Stop();
                    }
                }
            }

        private bool CheckBulletCollision()
        {
            if (pictureBox10.Bounds.IntersectsWith(pictureBox1.Bounds) ||
                pictureBox10.Bounds.IntersectsWith(pipeTop.Bounds) ||
                pictureBox10.Bounds.IntersectsWith(pictureBox2.Bounds) ||
                pictureBox10.Bounds.IntersectsWith(pictureBox4.Bounds) ||
                pictureBox10.Bounds.IntersectsWith(pictureBox9.Bounds) ||
                pictureBox10.Bounds.IntersectsWith(pictureBox8.Bounds) ||
                pictureBox10.Bounds.IntersectsWith(pictureBox13.Bounds) ||
                pictureBox10.Bounds.IntersectsWith(pictureBox14.Bounds))
            {
                return true; 
            }
            if (pictureBox10.Bounds.IntersectsWith(pictureBox3.Bounds) && pictureBox3.Visible)
            {
                boss2HitCount++;


                if (boss2HitCount >= 2)
                {
                    pictureBox3.Visible = false;
                    boss2HitCount = 0;
                    pictureBox3.Location = new Point(ClientSize.Width + 100, pictureBox3.Top); 
                    this.BackgroundImage = Image.FromFile("D:\\LeChiThanh_2122110144\\FlappyBird\\FlappyBird\\images\\1.png"); 
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }

                return true;
            }
            if (pictureBox10.Bounds.IntersectsWith(pictureBox12.Bounds) && pictureBox12.Visible)
            {
                bulletHitCount++;

                if (bulletHitCount >= 2)
                {
                    pictureBox12.Visible = false; 
                    bulletHitCount = 0; 
                    pictureBox12.Location = new Point(ClientSize.Width + 100, pictureBox12.Top);
                    this.BackgroundImage = Image.FromFile("D:\\LeChiThanh_2122110144\\FlappyBird\\FlappyBird\\images\\2.png");
                    this.BackgroundImageLayout = ImageLayout.Center;
                   
                }

                return true;
            }
            if (pictureBox10.Bounds.IntersectsWith(pictureBox5.Bounds) && pictureBox5.Visible)
            {
                boss3HitCount++;
                if (boss3HitCount >= 2)
                {
                    pictureBox5.Visible = false;
                    boss3HitCount = 0;
                    pictureBox5.Location = new Point(ClientSize.Width + 150, pictureBox5.Top);
                    winLabel.Location = new Point((ClientSize.Width - winLabel.Width) / 2, (ClientSize.Height - winLabel.Height) / 2); 
                    winLabel.Visible = true;
                    endGame();
                }

                return true;
            }

            return false;
        }
        private void ResetBullet()
            {
                pictureBox10.Left = flappyBird.Left + 50;
                pictureBox10.Top = flappyBird.Top + 20;
                bulletCount++;
            }

            private void ItemRespawnTimer_Tick(object sender, EventArgs e)
            {
                pictureBox11.Visible = true; 
                pictureBox11.Location = GetRandomItemPosition(); 
                itemCollected = false; 
                itemRespawnTimer.Stop(); 
            }

            private void ItemOutOfBoundsTimer_Tick(object sender, EventArgs e)
            {
                pictureBox11.Visible = true;
                pictureBox11.Location = GetRandomItemPosition(); 
                itemOutOfBoundsTimer.Stop(); 
            }

            private void endGame()
            {

            bulletTimer.Stop();
            itemRespawnTimer.Stop();
            itemOutOfBoundsTimer.Stop();
            scoreText.Text += " Game over!!!";
                fireBullet = false;
                gameTimer.Stop();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                TogglePause();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void TogglePause()
        {
            if (isPaused)
            {
                gameTimer.Start();
                bulletTimer.Start();
                itemRespawnTimer.Start();
                itemOutOfBoundsTimer.Start();
                isPaused = false;
            }
            else
            {
                StopGame();
                isPaused = true;
            }
        }
        private void StopGame()
        {
            gameTimer.Stop();
            bulletTimer.Stop();
            itemRespawnTimer.Stop(); 
            itemOutOfBoundsTimer.Stop(); 
            ShowPauseMenu();
        }
        private void ShowPauseMenu()
        {           
            groupBox1.Visible = true;                            
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Space)
                {
                    gravity = 5;
                }
            }

            private void Form1_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Space)
                {
                    gravity = -8 ;
                }
                else if (e.KeyCode == Keys.R)
                {
                    restartGame();
                }
            }
            private void restartGame()
            {
                flappyBird.Top = 200;
                pictureBox1.Left = 800;
                pipeTop.Left = 900;
                pictureBox2.Left = 950;
                pictureBox4.Left = 1000;
                pictureBox9.Left = 1050;
                pictureBox8.Left = 1100;
                pictureBox13.Left = 1150;
                pictureBox14.Left = 1200;
                pipeSpeed = 8;
                gravity = 15;
                score = 0;
                bulletHitCount = 0;
                boss2HitCount = 0;
                boss3HitCount = 0;
                scoreText.Text = "Score: " + score;
                pictureBox11.Visible = false;
                itemAppeared = false;
                itemCollected = false;
                bulletCount = 0;
                pictureBox10.Visible = false;
                fireBullet = false;

                gameTimer.Start();
            }

            private void Form1_Load(object sender, EventArgs e)
            {
               
                flappyBird.Top = 200;
                gameTimer.Start();
            }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false; 
            if (isPaused)
            {
                TogglePause(); 
            }
            gameTimer.Start();
            bulletTimer.Start();
            itemRespawnTimer.Start();
            itemOutOfBoundsTimer.Start();
            this.KeyDown -= Form1_KeyDown; 
            this.KeyDown += Form1_KeyDown;

            this.KeyUp -= Form1_KeyUp;
            this.KeyUp += Form1_KeyUp;
           
            fireBullet = true; 
            this.Focus();  
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
        }
        private void pictureBox10_Click_1(object sender, EventArgs e)
        {
        }
    }
    }
