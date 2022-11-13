namespace SpaceInvaders.UI
{
    class GameOver
    {
        private const string DefeatFile = @"Assets/defeatFile.txt";
        private const string VictoryFile = @"Assets/victoryFile.txt";
        public static void DrawDefeat()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), DefeatFile);
            DrawArt(filePath);
        }
        public static void DrawVictory()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), VictoryFile);
            DrawArt(filePath);
        }
        private static void DrawArt(string filePath)
        {
            string art = File.ReadAllText(filePath);
            Console.Clear();
            Console.WriteLine(art);
            Thread.Sleep(1000);
            Console.WriteLine("\n\t  Press any key to exit...");
            Console.ReadKey(true);
        }
    }
}