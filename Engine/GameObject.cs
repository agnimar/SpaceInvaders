namespace SpaceInvaders.Engine
{
    class GameObject
    {
        private bool destroyed;
        public bool Destroyed { get => destroyed; }
        public GameObject()
        {
            Application.RegisterObject(this);
        }
        public virtual void Init()
        {

        }
        public virtual void Start()
        {

        }
        public virtual void Update(TimeSpan deltaTime)
        {

        }
        public void Destroy()
        {
            destroyed = true;
            Application.DeregisterObject(this);
        }
    }   
}