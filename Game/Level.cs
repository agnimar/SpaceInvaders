using SpaceInvaders.Engine;

namespace SpaceInvaders.Game
{
    class Level : GameObject
    {
        public const char EnemyIcon = 'H';
        public const char EnemyBulletIcon = '!';
        public const char PlayerIcon = 'A';
        private const char PlayerBulletIcon = '^';
        private const string LevelFile = @"Assets/levelFile.txt";
        // Singleton
        private static object locker = new object();
        private static Level? instances = null;
        public static Level Instances
        {
            get
            {
                lock (locker)
                {
                    if (instances == null)
                        instances = new Level();
                    return instances;
                }
            }
        }
        public int Score {get; private set;}
        private List<Entity> children = new List<Entity>();
        public int LevelWidth {get; private set;}
        public int LevelHeight {get; private set;}
        private Player? player;
        public List<Entity> Children {get => children;}
        private List<Enemy> enemies = new List<Enemy>();
        private Random rand = new Random();
        private double minTimeToFire = 0.1;
        private double maxTimeToFire = 2.0;
        private double nextFireTimer = 1.0;
        public State FinalState {get; private set;}

        public override void Init()
        {
            // Load
            LoadLevelFromFile();
            // Create objects
            player = new Player(PlayerIcon);
            children.Add(player);
            Score =  0;
        }
        public override void Update(TimeSpan deltaTime)
        {
            for (int i = children.Count - 1; i >= 0 ; i--)
            {
                // Destroy
                if (children[i].Destroyed == true)
                {
                    if (children[i] is Enemy)
                        enemies.Remove((Enemy)children[i]);
                    children.RemoveAt(i);   
                }          
            }
            // Check for game end
            if (enemies.Count == 0)
            {
                FinalState = State.Victory;
                Application.StopGame();
                instances = null;
                return;
            }
            else if (player.Hp <= 0) {
                FinalState = State.Defeat;
                Application.StopGame();
                instances = null;
                return;
            }
            else
                // Not ended = Fire away
                FireEnemyBullet(deltaTime);
        }
        public int GetPlayerHp() {
            return player.Hp;
        }
        private void FireEnemyBullet(TimeSpan deltaTime)
        {
            nextFireTimer -= deltaTime.TotalSeconds;
            if (nextFireTimer > 0.0)
                return;

            List<Enemy> front = new List<Enemy>();
            foreach (var enemy in enemies)
            {
                if (enemies.FindAll(e => e.X == enemy.X).Count(e => e.Y > enemy.Y) == 0)
                    front.Add(enemy);
            }
            int enemyIndex = rand.Next(front.Count);
            Enemy firingEnemy = front[enemyIndex];
            Bullet bullet = new Bullet(EnemyBulletIcon, firingEnemy.X, firingEnemy.Y + 1, +1);
            children.Add(bullet);

            nextFireTimer = minTimeToFire + rand.NextDouble() * (maxTimeToFire - minTimeToFire);
        }
        private void LoadLevelFromFile()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), LevelFile);
            string[] levelData = File.ReadAllLines(filePath);
            // -2 => ignore border data
            LevelWidth = levelData[0].Length - 2;
            LevelHeight = levelData.Length - 2;
            // - 3 => leave space for above and below Player
            int moveDownLimit = levelData.Count(s => string.IsNullOrWhiteSpace(s.Replace('\n', ' '))) - 3;
            for (int x = 0; x < levelData[0].Length; x++)
            {
                for (int y = 0; y < levelData.Length; y++)
                {
                    if (levelData[y][x] == EnemyIcon)
                    {
                        Enemy enemy = new Enemy(EnemyIcon, x, y, moveDownLimit);
                        children.Add(enemy);
                        enemies.Add(enemy);
                    }
                }
            }

        }
        public Entity? Collide(Entity self, int x, int y)
        {
            for (int i = children.Count() - 1; i >= 0; i--)
            {
                var entity = children[i];
                if (entity == self)
                    continue;
                if (entity.X == x && entity.Y == y)
                {
                    return entity;
                }
            }
            return null;
        }
        public void DamageEntity(Entity entity)
        {
            if (entity == player)
            {
                Score -= 1000;
                player.Damage();
            }
            else
            {
                Score += 100;
                entity.Destroy();
            }
        }

        internal void SpawnPlayerBullet(int x, int y)
        {
            Bullet bullet = new Bullet(PlayerBulletIcon, x, y, -1);
            children.Add(bullet);
        }
        public void StopGame()
        {
            FinalState = State.MainMenu;
            Application.StopGame();
            instances = null;
            return;
        }
    }
}