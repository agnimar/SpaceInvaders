using SpaceInvaders.Engine;

namespace SpaceInvaders.Game
{
    class Bullet : Entity
    {
        
        public Bullet(char icon, int x, int y, int direction)
        {
            this.icon = icon;
            this.x = x;
            this.y = y;
            this.direction = direction;
            level = Level.Instances;
            mapHeight = level.LevelHeight;
        }

        public override char Icon => icon;

        public override int X { get => x; protected set => x = value; }
        public override int Y { get => y; protected set => y = value; }

        private double moveSpeed = 0.2;
        private double moveTimer = 0.0;
        private Level level;
        private int direction;
        private int mapHeight;
        public override void Update(TimeSpan deltaTime)
        {
            if (Destroyed)
                return;
            moveTimer += deltaTime.TotalSeconds;
            if (moveTimer > moveSpeed)
            {
                moveTimer = 0.0;
                if (Y > 0 && Y < mapHeight - 1)
                    Y += direction;
                else
                    Destroy();
            }
            Entity? collisionEntity = level.Collide(this, X, Y);
            if (collisionEntity is not null)
            {
                Destroy();
                level.DamageEntity(collisionEntity);
            }
        }
    }
}