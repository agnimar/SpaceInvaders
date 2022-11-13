using SpaceInvaders.Engine;

namespace SpaceInvaders.Game
{
    class Enemy : Entity
    {
        public override char Icon => icon;
        public override int X { get => x; protected set => x = value; }
        public override int Y { get => y; protected set => y = value; }
        private int mapHeight;
        private int mapWidth;
        private Level level;

        private double moveSpeed = 1.5;
        private double moveTimer = 0.0;
        // How many time to move before switching direction
        private int moveSpaceLimit = 5;
        private int moveSpaceCounter = 0;
        private int moveDownLimit;
        // 1 or -1
        private int moveDirection = 1;
        public Enemy(char icon, int x, int y, int moveDownLimit)
        {
            level = Level.Instances;
            mapHeight = level.LevelHeight;
            mapWidth = level.LevelWidth;
            this.icon = icon;
            this.x = x;
            this.y = y;
            this.moveDownLimit = moveDownLimit;
        }

        public override void Update(TimeSpan deltaTime)
        {
            moveTimer += deltaTime.TotalSeconds;
            if (moveTimer > moveSpeed)
            {
                moveTimer = 0.0;
                // move down
                if (moveSpaceCounter == moveSpaceLimit)
                {
                    // -4 => leave space for the player character
                    if (level.Collide(this, X, Y + 1) is null && Y < mapHeight - 4)
                        Y += 1;
                    moveDirection *= -1;
                    moveSpaceCounter = 0;
                    return;
                } 
                //move to side
                else 
                {
                    if (level.Collide(this, X + moveDirection, Y) is null)
                        X += moveDirection;
                    moveSpaceCounter += 1;
                }
            }
        }
    }
}