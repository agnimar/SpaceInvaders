namespace SpaceInvaders.UI
{
    class Exit
    {
        private const string GoodbyeFile = @"Assets/goodbyeMenuFile.txt";
        public static void DrawExit()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), GoodbyeFile);
            string exitArt = File.ReadAllText(filePath);
            Console.Clear();
            Console.WriteLine(exitArt);
            Console.WriteLine("\n\t  Press any key to exit...");
            Console.ReadKey(true);
        }
    }
}