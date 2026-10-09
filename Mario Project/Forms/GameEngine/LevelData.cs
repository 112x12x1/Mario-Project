using System.Collections.Generic;
using System.Drawing;

namespace MarioGameSystem.Forms.GameEngine
{
    public class PlatformData
    {
        public Rectangle Bounds { get; set; }
        public Color Color { get; set; } = Color.SaddleBrown;
    }

    public class CoinData
    {
        public Point Location { get; set; }
    }

    public class EnemyData
    {
        public Point Location { get; set; }
        public int MoveRange { get; set; } = 100;
        public int Speed { get; set; } = 2;
    }

    public class LevelConfig
    {
        public int LevelNumber { get; set; }
        public string LevelName { get; set; } = string.Empty;
        public int TimeLimit { get; set; }
        public int MapWidth { get; set; } = 2400;
        public Point StartPoint { get; set; }
        public Point GoalPoint { get; set; }
        public List<PlatformData> Platforms { get; set; } = new List<PlatformData>();
        public List<CoinData> Coins { get; set; } = new List<CoinData>();
        public List<EnemyData> Enemies { get; set; } = new List<EnemyData>();
    }

    public static class LevelRepository
    {
        public static LevelConfig GetLevel(int level)
        {
            switch (level)
            {
                case 1:
                    return new LevelConfig
                    {
                        LevelNumber = 1,
                        LevelName = "Level 1: Map Dài & Kẻ Địch Goomba",
                        TimeLimit = 90,
                        MapWidth = 2400,
                        StartPoint = new Point(50, 300),
                        GoalPoint = new Point(2250, 310),
                        Platforms = new List<PlatformData>
                        {
                            // Đất nền kéo dài
                            new PlatformData { Bounds = new Rectangle(0, 380, 800, 70) },
                            new PlatformData { Bounds = new Rectangle(900, 380, 700, 70) }, // Có vực rộng 100px ở x=800
                            new PlatformData { Bounds = new Rectangle(1700, 380, 700, 70) }, // Vực 2 ở x=1600

                            // Bậc cao trên không
                            new PlatformData { Bounds = new Rectangle(300, 270, 140, 20) },
                            new PlatformData { Bounds = new Rectangle(600, 200, 140, 20) },
                            new PlatformData { Bounds = new Rectangle(1100, 260, 160, 20) },
                            new PlatformData { Bounds = new Rectangle(1400, 190, 140, 20) },
                            new PlatformData { Bounds = new Rectangle(1850, 250, 150, 20) }
                        },
                        Coins = new List<CoinData>
                        {
                            new CoinData { Location = new Point(350, 230) },
                            new CoinData { Location = new Point(650, 160) },
                            new CoinData { Location = new Point(1150, 220) },
                            new CoinData { Location = new Point(1450, 150) },
                            new CoinData { Location = new Point(1900, 210) }
                        },
                        Enemies = new List<EnemyData>
                        {
                            new EnemyData { Location = new Point(450, 350), MoveRange = 120, Speed = 2 },
                            new EnemyData { Location = new Point(1000, 350), MoveRange = 150, Speed = 3 },
                            new EnemyData { Location = new Point(1420, 150), MoveRange = 80, Speed = 2 },
                            new EnemyData { Location = new Point(1800, 350), MoveRange = 160, Speed = 4 }
                        }
                    };

                case 2:
                    return new LevelConfig
                    {
                        LevelNumber = 2,
                        LevelName = "Level 2: Vực Sâu & Kẻ Địch Tốc Độ",
                        TimeLimit = 75,
                        MapWidth = 2600,
                        StartPoint = new Point(50, 300),
                        GoalPoint = new Point(2450, 110),
                        Platforms = new List<PlatformData>
                        {
                            new PlatformData { Bounds = new Rectangle(0, 380, 400, 70) },
                            new PlatformData { Bounds = new Rectangle(500, 380, 400, 70) },
                            new PlatformData { Bounds = new Rectangle(1000, 380, 500, 70) },
                            new PlatformData { Bounds = new Rectangle(1600, 380, 1000, 70) },

                            new PlatformData { Bounds = new Rectangle(250, 270, 120, 20) },
                            new PlatformData { Bounds = new Rectangle(650, 250, 120, 20) },
                            new PlatformData { Bounds = new Rectangle(1200, 200, 150, 20) },
                            new PlatformData { Bounds = new Rectangle(1800, 240, 120, 20) },
                            new PlatformData { Bounds = new Rectangle(2100, 170, 120, 20) },
                            new PlatformData { Bounds = new Rectangle(2400, 150, 150, 20) }
                        },
                        Coins = new List<CoinData>
                        {
                            new CoinData { Location = new Point(280, 230) },
                            new CoinData { Location = new Point(680, 210) },
                            new CoinData { Location = new Point(1250, 160) },
                            new CoinData { Location = new Point(2130, 130) }
                        },
                        Enemies = new List<EnemyData>
                        {
                            new EnemyData { Location = new Point(200, 350), MoveRange = 100, Speed = 3 },
                            new EnemyData { Location = new Point(600, 350), MoveRange = 120, Speed = 4 },
                            new EnemyData { Location = new Point(1100, 350), MoveRange = 180, Speed = 4 },
                            new EnemyData { Location = new Point(1750, 350), MoveRange = 200, Speed = 5 }
                        }
                    };

                default:
                    return GetLevel(1);
            }
        }
    }
}