using SpaceInvaders.Engine;

namespace SpaceInvaders.Game
{
    class Player : Entity
    {
        private Level level;
        private int mapWidth;
        private int mapHeight;
        public override char Icon => icon;
        private double shootCooldown = 0.5;
        private double currentCooldown = 0.0;
        private int maxHp = 3;
        public int Hp {get; private set;}

        public override int X { get => x; protected set => x  = value; }
        public override int Y { get => y; protected set => y = value; }
        public Player(char icon)
        {
            level = Level.Instances;
            this.icon = icon;
            mapWidth = level.LevelWidth;
            mapHeight = level.LevelHeight;
            x = mapWidth / 2;
            y = mapHeight - 2;
            Hp = maxHp;
        }

        public override void Update(TimeSpan deltaTime)
        {
            if (currentCooldown > 0)
                currentCooldown -= deltaTime.TotalSeconds;
            HandleInput();
        }
        private void MovePlayer(int direction)
        {
            int xCoord = X;
            xCoord += direction;
            if (xCoord < 0) 
                xCoord = 0;
            if (xCoord >= mapWidth)
                xCoord = mapWidth - 1;
            X = xCoord;
        }
        private void HandleInput()
        {
            if (Console.KeyAvailable == false)
                return;
            var cki = Console.ReadKey(true);
            switch (cki.Key)
            {
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    MovePlayer(-1);
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    MovePlayer(1);
                    break;
                case ConsoleKey.Spacebar:
                    Shoot();
                    break;
                case ConsoleKey.Escape:
                    level.StopGame();
                    break;
                default:
                    break;
            }
        }
        private void Shoot()
        {
            if (currentCooldown > 0)
                return;
            currentCooldown = shootCooldown;
            level.SpawnPlayerBullet(X, Y - 1);
        }
        public void Damage()
        {
            Hp -= 1;
        }
    }
}