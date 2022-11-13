using SpaceInvaders.Engine;
using SpaceInvaders.Game;

namespace SpaceInvaders.UI
{
    class LevelUi : GameObject
    {
        private const string BannerFile = @"Assets/levelBannerFile.txt";
        private const string LevelFile = @"Assets/levelFile.txt";
        private const string HeartIcon = "X";
        private Level level;
        private string bannerArt;
        private string levelArt;

        public LevelUi()
        {
            this.level = Level.Instances;
            string bannerFilePath = Path.Combine(Directory.GetCurrentDirectory(), BannerFile);
            this.bannerArt = File.ReadAllText(bannerFilePath);
            string levelFilePath = Path.Combine(Directory.GetCurrentDirectory(), LevelFile);
            this.levelArt = File.ReadAllText(levelFilePath).Replace(Level.EnemyIcon, ' ');
        }
        public override void Start()
        {
            Console.Clear();
        }

        public override void Update(TimeSpan deltaTime)
        {
            // Clear screen
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(bannerArt);
            Console.SetCursorPosition(0, bannerArt.Split('\n').Length - 1);
            Console.Write(levelArt);
            // Score
            Console.SetCursorPosition(9, 1);
            Console.Write(level.Score);
            // Lives
            Console.SetCursorPosition(9, 2);
            string life = "";
            for (int i = 0; i < level.GetPlayerHp(); i++)
                life += HeartIcon + ' ';
            Console.Write(life);
            // Characters
            foreach (var child in level.Children)
            {
                Console.SetCursorPosition(child.X + 1, child.Y + 4);
                Console.Write(child.Icon);
            }
        }
    }
}