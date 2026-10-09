using System.Drawing;
using System.Windows.Forms;

namespace MarioGameSystem.Forms.GameEngine
{
    public class EnemyControl
    {
        public PictureBox Picture { get; set; }
        public int StartX { get; set; }
        public int MoveRange { get; set; }
        public int Speed { get; set; }
        public bool MovingRight { get; set; } = true;
        public bool IsAlive { get; set; } = true;

        public EnemyControl(Point location, int moveRange, int speed)
        {
            StartX = location.X;
            MoveRange = moveRange;
            Speed = speed;

            Picture = new PictureBox
            {
                Size = new Size(30, 30),
                Location = location,
                BackColor = Color.Purple,
                Tag = "enemy"
            };
        }

        public void UpdateMovement()
        {
            if (!IsAlive || !Picture.Visible) return;

            if (MovingRight)
            {
                Picture.Left += Speed;
                if (Picture.Left >= StartX + MoveRange) MovingRight = false;
            }
            else
            {
                Picture.Left -= Speed;
                if (Picture.Left <= StartX) MovingRight = true;
            }
        }
    }
}