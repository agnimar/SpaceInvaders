using SpaceInvaders.Engine;

namespace SpaceInvaders.Game
{
    abstract class Entity : GameObject
    {
        protected char icon;
        protected int x;
        protected int y;
        public abstract char Icon { get; }
        public abstract int X { get; protected set; }
        public abstract int Y { get; protected set; }
    }
}