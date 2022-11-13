namespace SpaceInvaders.Engine
{
    class Application
    {
        private static List<GameObject> registeredObjects = new List<GameObject>();
        public static bool Running { get; private set; }
        public void Run()
        {
            // Set GameLoop to run
            Running = true;
            for (int i = 0; i < registeredObjects.Count; i++)
            {
                GameObject obj = registeredObjects[i];
                obj.Init();
            }

            for (int i = 0; i < registeredObjects.Count; i++)
            {
                GameObject obj = registeredObjects[i];
                obj.Start();
            }

            DateTime previousGameTime = DateTime.Now;
            while (Running)
            {
                // Calculate the time elapsed since the last game loop cycle
                TimeSpan deltaTime = DateTime.Now - previousGameTime;
                // Update the current previous game time
                previousGameTime += deltaTime;
                // Run Updates
                for (int i = 0; i < registeredObjects.Count; i++)
                {
                    GameObject obj = registeredObjects[i];
                    obj.Update(deltaTime);
                }
                Thread.Sleep(20);
            }
        }
        public static void RegisterObject(GameObject obj)
        {
            registeredObjects.Add(obj);
        }
        public static void DeregisterObject(GameObject obj)
        {
            registeredObjects.Remove(obj);
        }
        public static void StopGame()
        {
            Running = false;
            registeredObjects.Clear();
        }
    }

}